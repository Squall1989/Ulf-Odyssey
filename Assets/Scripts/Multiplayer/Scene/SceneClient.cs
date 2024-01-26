

using MsgPck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private List<Planet> planets = new();

    public SceneClient(INetworkable networkable, string playerId)
    {
        _playerId = playerId;
        _networkable = networkable;
        _networkable.RegisterHandler<SnapSceneStruct>(SceneReceived);
        
    }

    public void AddPlanet(Planet planet)
    {
        planets.Add(planet);
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

    public Task<SnapPlayerStruct> SpawnPlayer()
    {
        throw new NotImplementedException();
    }
}
