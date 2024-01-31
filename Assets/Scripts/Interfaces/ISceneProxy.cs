using MsgPck;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ulf;

public interface ISceneProxy
{
    Task<List<SnapPlanetStruct>> GetSceneStruct();
    Task<SnapPlayerStruct> SpawnPlayer();
    void AddPlanet(Planet planet);
    void AddPlayer(Player player, bool isOurPlayer);
    void DoPlayerAction(ActionData playerActionData);
    void CreatePlayerMoveAction(int direct);
    void CreatePlayerStandAction(int direct);
}
