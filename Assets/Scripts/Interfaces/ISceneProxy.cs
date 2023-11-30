using MsgPck;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ulf;

public interface ISceneProxy
{
    Task<List<SnapPlanetStruct>> GetSceneStruct();
    void AddPlanet(Planet planet);
}
