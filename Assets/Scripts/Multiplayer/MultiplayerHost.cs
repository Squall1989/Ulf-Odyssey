
using MsgPck;
using System.Collections.Generic;
using Ulf;
using Unity.Networking.Transport;

public class MultiplayerHost 
{
    private ISceneProxy _sceneHost;
    private INetworkable _networkable;
    protected Dictionary<string, PlayerData> _players = new();
    protected Dictionary<NetworkConnection, string> _connetions = new();
    private UnitsBehaviour _unitsBehaviour;

    public MultiplayerHost(INetworkable networkable, ISceneProxy sceneHost, IUnitsProxy unitsBehaviour) 
    {
        _sceneHost = sceneHost;
        _networkable = networkable;
        _networkable.RegisterHandler<PlayerData>(PlayerReady);
        _unitsBehaviour = (UnitsBehaviour)unitsBehaviour;

        _unitsBehaviour.OnUnitAction += SendAction;
    }

    private void SendAction(int unitGuid, INextAction nextAction)
    {
        _networkable.Send(new ActionData()
        {
            action = nextAction,
            guid = unitGuid,
        });
    }

    private async void PlayerReady(PlayerData msg, NetworkConnection connection)
    {
        string id = msg.playerId;
        _connetions.Add(connection, id);
        _players.Add(id, msg);

        var sceneStruct = await _sceneHost.GetSceneStruct();

        for (int i = 0; i < sceneStruct.Count; i++)
        {
            _networkable.Send(new SnapSceneStruct()
            {
                 totalCount = sceneStruct.Count,
                 currentCount = i,
                 snapPlanet = sceneStruct[i]
            }, connection);
        }
    }
}
