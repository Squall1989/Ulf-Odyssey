
namespace Ulf
{
    public class MultiplayerEntry : IGameEntry
    {
        protected int playerId;

        public void SetId(int id)
        {
            playerId = id;
        }
    }
}