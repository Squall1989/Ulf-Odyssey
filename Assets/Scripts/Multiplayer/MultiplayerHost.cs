
using MsgPck;
using System;
using System.Collections.Generic;
using Ulf;
using Unity.Networking.Transport;

public class MultiplayerHost 
{
    private IPlayerProxy _playerProxy;
    private ISceneProxy _sceneHost;
    private INetworkable _networkable;
    protected Dictionary<string, PlayerData> _players = new();
    protected Dictionary<IConnectWrapper, string> _connetions = new();
    private UnitsBehaviour _unitsBehaviour;

    public MultiplayerHost(INetworkable networkable, ISceneProxy sceneHost, IUnitsProxy unitsBehaviour, IPlayerProxy playerProxy) 
    {
        _playerProxy = playerProxy;
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

    private async void PlayerReady(PlayerData msg, IConnectWrapper connection)
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

        var players = _playerProxy.PlayersSnapshot();
        foreach (var player in players)
            _networkable.Send(player, connection);
    }
}
