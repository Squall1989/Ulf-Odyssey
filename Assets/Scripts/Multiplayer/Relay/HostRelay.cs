using System;
using Unity.Collections;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace Ulf
{
    public class HostRelay 
    {
        private NativeList<NetworkConnection> serverConnections;
        private Allocation hostAllocation;
        private NetworkDriver hostDriver;
        private string joinCode;

        public Action<string> OnCodeGenerate;
        public Action<string> OnLog;


        public void BindHost()
        {
            OnLog?.Invoke("Host - Binding to the Relay server using UTP.");

            // Extract the Relay server data from the Allocation response.
            var relayServerData = new RelayServerData(hostAllocation, "udp");

            // Create NetworkSettings using the Relay server data.
            var settings = new NetworkSettings();
            settings.WithRelayParameters(ref relayServerData);

            // Create the Host's NetworkDriver from the NetworkSettings.
            hostDriver = NetworkDriver.Create(settings);

            // Bind to the Relay server.
            if (hostDriver.Bind(NetworkEndpoint.AnyIpv4) != 0)
            {
                OnLog?.Invoke("Host client failed to bind");
            }
            else
            {
                if (hostDriver.Listen() != 0)
                {
                    OnLog?.Invoke("Host client failed to listen");
                }
                else
                {
                    OnLog?.Invoke("Host client bound to Relay server");
                }
            }

            JoinCode();
        }

        public async void StartAllocate()
        {
            OnLog?.Invoke("Host - Creating an allocation. Upon success, I have 10 seconds to BIND to the Relay server that I've allocated.");

            // Determine region to use (user-selected or auto-select/QoS)
            //string region = GetRegionOrQosDefault();
            //OnLog?.Invoke($"The chosen region is: {region ?? autoSelectRegionName}");

            // Set max connections. Can be up to 100, but note the more players connected, the higher the bandwidth/latency impact.
            int maxConnections = 4;

            // Important: After the allocation is created, you have ten seconds to BIND, else the allocation times out.
            hostAllocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            OnLog?.Invoke($"Host Allocation ID: {hostAllocation.AllocationId}, region: {hostAllocation.Region}");

            // Initialize NetworkConnection list for the server (Host).
            // This list object manages the NetworkConnections which represent connected players.
            serverConnections = new NativeList<NetworkConnection>(maxConnections, Allocator.Persistent);
        
            BindHost();
        }

        public async void JoinCode()
        {
            OnLog?.Invoke("Host - Getting a join code for my allocation. I would share that join code with the other players so they can join my session.");

            try
            {
                joinCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocation.AllocationId);
                OnLog?.Invoke("Host - Got join code: " + joinCode);
                OnCodeGenerate?.Invoke(joinCode);
            }
            catch (RelayServiceException ex)
            {
                OnLog?.Invoke(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Update()
        {
            UpdateHost();
        }

        void UpdateHost()
        {
            // Skip update logic if the Host isn't yet bound.
            if (!hostDriver.IsCreated || !hostDriver.Bound)
            {
                return;
            }

            // This keeps the binding to the Relay server alive,
            // preventing it from timing out due to inactivity.
            hostDriver.ScheduleUpdate().Complete();

            // Clean up stale connections.
            for (int i = 0; i < serverConnections.Length; i++)
            {
                if (!serverConnections[i].IsCreated)
                {
                    OnLog?.Invoke("Stale connection removed");
                    serverConnections.RemoveAt(i);
                    --i;
                }
            }

            // Accept incoming client connections.
            NetworkConnection incomingConnection;
            while ((incomingConnection = hostDriver.Accept()) != default(NetworkConnection))
            {
                // Adds the requesting Player to the serverConnections list.
                // This also sends a Connect event back the requesting Player,
                // as a means of acknowledging acceptance.
                OnLog?.Invoke("Accepted an incoming connection.");
                serverConnections.Add(incomingConnection);
                SendMessage("Hello!!!!!!!!");
            }

            // Process events from all connections.
            for (int i = 0; i < serverConnections.Length; i++)
            {
                //Assert.IsTrue(serverConnections[i].IsCreated);

                // Resolve event queue.
                NetworkEvent.Type eventType;
                while ((eventType = hostDriver.PopEventForConnection(serverConnections[i], out var stream)) != NetworkEvent.Type.Empty)
                {
                    switch (eventType)
                    {
                        // Handle Relay events.
                        case NetworkEvent.Type.Data:
                            FixedString32Bytes msg = stream.ReadFixedString32();
                            OnLog?.Invoke($"Server received msg: {msg}");
                            //hostLatestMessageReceived = msg.ToString();
                            break;

                        // Handle Disconnect events.
                        case NetworkEvent.Type.Disconnect:
                            OnLog?.Invoke("Server received disconnect from client");
                            serverConnections[i] = default(NetworkConnection);
                            break;
                    }
                }
            }
        }

        public void SendMessage(string msg)
        {
            if (serverConnections.Length == 0)
            {
                OnLog?.Invoke("No players connected to send messages to.");
                return;
            }

            // Get message from the input field, or default to the placeholder text.
            //var msg = !String.IsNullOrEmpty(HostMessageInput.text) ? HostMessageInput.text : HostMessageInput.placeholder.GetComponent<Text>().text;

            // In this sample, we will simply broadcast a message to all connected clients.
            for (int i = 0; i < serverConnections.Length; i++)
            {
                if (hostDriver.BeginSend(serverConnections[i], out var writer) == 0)
                {
                    // Send the message. Aside from FixedString32, many different types can be used.
                    writer.WriteFixedString32(msg);
                    hostDriver.EndSend(writer);
                }
            }
        }
    }
}