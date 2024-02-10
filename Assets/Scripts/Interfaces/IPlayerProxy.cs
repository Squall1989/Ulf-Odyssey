using System.Threading.Tasks;

namespace Ulf
{
    public interface IPlayerProxy
    {
        void AddPlayer(Player player, bool isOurPlayer);
        Task<SnapPlayerStruct> SpawnPlayer();
        void CreatePlayerMoveAction(int direct);
        void CreatePlayerStandAction(int id, float angle);
    }
}