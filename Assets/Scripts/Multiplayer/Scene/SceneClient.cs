

using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ulf;
/// <summary>
/// Connect to scene hoster
/// </summary>
public class SceneClient : ISceneProxy
{
    private string _playerId;
    private INetworkable _networkable;
    private int totalPlanetCount = 999;
    private List<SnapPlanetStruct> planetSnaps = new();

    public SceneClient(INetworkable networkable, string playerId)
    {
        _playerId = playerId;
        _networkable = networkable;
        _networkable.RegisterHandler<SnapSceneStruct>(SceneReceived);
        
    }

    public async Task<List<SnapPlanetStruct>> GetSceneStruct()
    {

        while(!_networkable.IsConnected)
        {
            await Task.Yield();
        }

        _networkable.Send<IUnionMsg>(new PlayerData()
        {
            isReady = true,
            playerId = _playerId,
        });

        while (planetSnaps.Count < totalPlanetCount)
        {
            await Task.Yield();
        };

        return planetSnaps;
    }

    void SceneReceived(SnapSceneStruct msg)
    {
        totalPlanetCount = msg.totalCount;
        planetSnaps.Add(msg.snapPlanet);
    }
}
