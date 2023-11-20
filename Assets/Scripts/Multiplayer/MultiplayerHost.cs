
using MsgPck;
using System.Collections.Generic;
using Unity.Networking.Transport;

public class MultiplayerHost 
{
    private INetworkable _networkable;
    private int idCount = 0;
    protected Dictionary<string, PlayerData> _players = new();
    protected Dictionary<NetworkConnection, string> _connetions = new();

    public MultiplayerHost(INetworkable networkable) 
    {
        _networkable = networkable;
        _networkable.RegisterHandler<PlayerReadyMsg>(PlayerReady);

    }


    private void PlayerReady(PlayerReadyMsg msg)
    {
        
    }
}
