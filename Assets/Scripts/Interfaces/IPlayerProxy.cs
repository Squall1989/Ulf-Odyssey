using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ulf
{
    public interface IPlayersProxy
    {
        List<SnapPlayerStruct> PlayersSnapshot();
        Action<SnapPlayerStruct> OnOtherPlayerSpawn { get; set; }
        void AddPlayer(Player player, bool isOurPlayer);
        Task<SnapPlayerStruct> SpawnPlayer();
        void CreatePlayerMoveAction(int direct);
        void CreatePlayerStandAction(int id, float angle);
        void CreateUniversalAction(ActionType actionType, int num);
    }
}