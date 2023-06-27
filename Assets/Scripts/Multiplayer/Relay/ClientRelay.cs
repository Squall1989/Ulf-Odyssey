using Unity.Networking.Transport.Relay;
using Unity.Networking.Transport;
using Unity.Collections;
using System;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

public class ClientRelay 
{

    private NetworkDriver playerDriver;
    private NetworkConnection clientConnection;
    private JoinAllocation playerAllocation;

    public Action<string> OnLog;


    public void BindPlayer()
    {
        OnLog?.Invoke("Player - Binding to the Relay server using UTP.");

        // Extract the Relay server data from the Join Allocation response.
        var relayServerData = new RelayServerData(playerAllocation, "udp");
        // Create NetworkSettings using the Relay server data.
        var settings = new NetworkSettings();
        settings.WithRelayParameters(ref relayServerData);

        // Create the Player's NetworkDriver from the NetworkSettings object.
        playerDriver = NetworkDriver.Create(settings);

        // Bind to the Relay server.
        if (playerDriver.Bind(NetworkEndpoint.AnyIpv4) != 0)
        {
            OnLog?.Invoke("Player client failed to bind");
        }
        else
        {
            OnLog?.Invoke("Player client bound to Relay server");
            clientConnection = playerDriver.Connect();

        }
    }

    public async void Join(string JoinCodeInput)
    {
        // Input join code in the respective input field first.
        if (String.IsNullOrEmpty(JoinCodeInput))
        {
            OnLog?.Invoke("Please input a join code.");
            return;
        }

        OnLog?.Invoke("Player - Joining host allocation using join code. Upon success, I have 10 seconds to BIND to the Relay server that I've allocated.");

        try
        {
            this.playerAllocation = await RelayService.Instance.JoinAllocationAsync(JoinCodeInput);
            OnLog?.Invoke("Player Allocation ID: " + playerAllocation.AllocationId);
            BindPlayer();
        }
        catch (RelayServiceException ex)
        {
            OnLog?.Invoke(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private void Update()
    {
        UpdatePlayer();
    }

    void UpdatePlayer()
    {
        // Skip update logic if the Player isn't yet bound.
        if (!playerDriver.IsCreated || !playerDriver.Bound)
        {
            return;
        }

        // This keeps the binding to the Relay server alive,
        // preventing it from timing out due to inactivity.
        playerDriver.ScheduleUpdate().Complete();

        // Resolve event queue.
        NetworkEvent.Type eventType;
        while ((eventType = clientConnection.PopEvent(playerDriver, out var stream)) != NetworkEvent.Type.Empty)
        {
            switch (eventType)
            {
                // Handle Relay events.
                case NetworkEvent.Type.Data:
                    FixedString32Bytes msg = stream.ReadFixedString32();
                    OnLog?.Invoke($"Player received msg: {msg}");
                    break;

                // Handle Connect events.
                case NetworkEvent.Type.Connect:
                    OnLog?.Invoke("Player connected to the Host");
                    break;

                // Handle Disconnect events.
                case NetworkEvent.Type.Disconnect:
                    OnLog?.Invoke("Player got disconnected from the Host");
                    clientConnection = default(NetworkConnection);
                    break;
            }
        }
    }
}
