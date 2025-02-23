
using MsgPck;
using System;
using System.Collections.Generic;
using Ulf;

public class MultiplayerHost 
{
    private IPlayersProxy _playerProxy;
    private ISceneProxy _sceneHost;
    private INetworkable _networkable;
    protected Dictionary<string, PlayerData> _players = new();
    protected Dictionary<IConnectWrapper, string> _connetions = new();
    private UnitsBehaviour _unitsBehaviour;

    public Action<IConnectWrapper> OnPlayerReady;

    public MultiplayerHost(INetworkable networkable, ISceneProxy sceneHost, IUnitsProxy unitsBehaviour) 
    {
        _sceneHost = sceneHost;
        _networkable = networkable;
        _networkable.RegisterHandler<PlayerData>(PlayerReady);
        _unitsBehaviour = (UnitsBehaviour)unitsBehaviour;

        _unitsBehaviour.OnUnitAction += SendAction;
    }

    public string GetPlayerId(IConnectWrapper connectWrapper)
    {
        if(_connetions.TryGetValue(connectWrapper, out var id)) 
            return id;
        else
            return null;
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
        if (_connetions.ContainsKey(connection))
        {
            _connetions[connection] = id;
        }
        else
        {
            _connetions.Add(connection, id);
        }
        if (_players.ContainsKey(id))
        {
            _players[id] = msg;
        }
        else
        {
            _players.Add(id, msg);
        }
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

        OnPlayerReady?.Invoke(connection);
    }
}
