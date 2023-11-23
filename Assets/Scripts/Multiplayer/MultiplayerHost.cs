
using MsgPck;
using System.Collections.Generic;
using Ulf;
using Unity.Networking.Transport;

public class MultiplayerHost 
{
    private SceneHost _sceneHost;
    private INetworkable _networkable;
    protected Dictionary<string, PlayerData> _players = new();
    protected Dictionary<NetworkConnection, string> _connetions = new();

    public MultiplayerHost(INetworkable networkable, SceneHost sceneHost) 
    {
        _sceneHost = sceneHost;
        _networkable = networkable;
        _networkable.RegisterHandler<PlayerReadyMsg>(PlayerReady);

    }


    private async void PlayerReady(PlayerReadyMsg msg, NetworkConnection connection)
    {
        string id = msg.playerData.playerId;
        _connetions.Add(connection, id);
        _players.Add(id, msg.playerData);

        var sceneStruct = await _sceneHost.GetSceneStruct();
        var sceneMsg = new SceneSnapMsg()
        { 
             sceneStruct = sceneStruct,
        };
        _networkable.Send(sceneMsg, connection);
    }
}
