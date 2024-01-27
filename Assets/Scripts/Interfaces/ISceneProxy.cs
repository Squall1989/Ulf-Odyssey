using MsgPck;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ulf;

public interface ISceneProxy
{
    Task<List<SnapPlanetStruct>> GetSceneStruct();
    Task<SnapPlayerStruct> SpawnPlayer();
    void AddPlanet(Planet planet);
}
