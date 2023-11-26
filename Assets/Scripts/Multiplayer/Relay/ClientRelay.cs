using Unity.Networking.Transport.Relay;
using Unity.Networking.Transport;
using Unity.Collections;
using System;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using MsgPck;
using Ulf;

public class ClientRelay : RelayBase, INetworkable
{

    private NetworkDriver clientDriver;
    private NetworkConnection clientConnection;
    private JoinAllocation playerAllocation;

    public Action OnJoined;
    private bool isActive;
    private bool isConnected;

    public Action<string> OnReceive { get; set; }

    public bool IsConnected => isConnected;

    public void BindPlayer()
    {
        OnLog?.Invoke("Player - Binding to the Relay server using UTP.");

        // Extract the Relay server data from the Join Allocation response.
        var relayServerData = new RelayServerData(playerAllocation, "udp");
        // Create NetworkSettings using the Relay server data.
        var settings = new NetworkSettings();
        settings.WithRelayParameters(ref relayServerData);

        // Create the Player's NetworkDriver from the NetworkSettings object.
        clientDriver = NetworkDriver.Create(settings);

        // Bind to the Relay server.
        if (clientDriver.Bind(NetworkEndpoint.AnyIpv4) != 0)
        {
            OnLog?.Invoke("Player client failed to bind");
        }
        else
        {
            OnLog?.Invoke("Player client bound to Relay server");
            clientConnection = clientDriver.Connect();
            isActive = true;
            QuickJoinLobby();
            OnJoined?.Invoke();
            Update();
        }
    }

    public async void Join(string JoinCodeInput)
    {
        // Input join code in the respective input field first.
        if (String.IsNullOrEmpty(JoinCodeInput))
        {
            OnLog?.Invoke("Try connect to random lobby...");
            QuickJoinLobby();
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

    public async void Update()
    {
        while (isActive)
        {
            UpdatePlayer();
            await Task.Delay(100);
        }
    }

    void UpdatePlayer()
    {
        // Skip update logic if the Player isn't yet bound.
        if (!clientDriver.IsCreated || !clientDriver.Bound)
        {
            return;
        }

        // This keeps the binding to the Relay server alive,
        // preventing it from timing out due to inactivity.
        clientDriver.ScheduleUpdate().Complete();

        // Resolve event queue.
        NetworkEvent.Type eventType;
        while ((eventType = clientConnection.PopEvent(clientDriver, out var stream)) != NetworkEvent.Type.Empty)
        {
            switch (eventType)
            {
                // Handle Relay events.
                case NetworkEvent.Type.Data:
                    //FixedString32Bytes msg = stream.ReadFixedString32();
                    //OnLog?.Invoke($"Player received msg: {msg}");
                    Read(stream, clientConnection);

                    break;

                // Handle Connect events.
                case NetworkEvent.Type.Connect:
                    OnLog?.Invoke("Player connected to the Host");
                    isConnected = true;
                    break;

                // Handle Disconnect events.
                case NetworkEvent.Type.Disconnect:
                    OnLog?.Invoke("Player got disconnected from the Host");
                    clientConnection = default(NetworkConnection);
                    isConnected = false;
                    break;
            }
        }
    }

    public void StopClient()
    {
        isActive = false;
    }

    private async void QuickJoinLobby()
    {
        try
        {
            // Quick-join a random lobby with a maximum capacity of 10 or more players.
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            options.Filter = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.MaxPlayers,
                    op: QueryFilter.OpOptions.GE,
                    value: "4")
            };

            var lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            var code = lobby.Data["code"].Value;
            OnLog?.Invoke("Lobby joined code: " + code);

            if (!string.IsNullOrEmpty(code))
            {
                Join(code);
            }
            // ...
        }
        catch (LobbyServiceException e)
        {
            OnLog?.Invoke(e.Message);
        }
    }


    protected override void SendToAll(NativeArray<byte> bytes)
    {
        if (clientDriver.BeginSend(clientConnection, out var writer) == 0)
        {
            writer.WriteBytes(bytes);
            clientDriver.EndSend(writer);
        }
    }

    protected override void SendTo(NativeArray<byte> bytes, NetworkConnection connection)
    {
        if (clientDriver.BeginSend(connection, out var writer) == 0)
        {
            writer.WriteBytes(bytes);
            clientDriver.EndSend(writer);
        }
    }
}
