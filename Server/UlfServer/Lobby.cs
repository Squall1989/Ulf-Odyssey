
using MsgPck;
using System.Collections.Generic;
using System.Linq;

namespace UlfServer
{
    public class Lobby
    {
        List<PlayerServer> _players = new List<PlayerServer>();
        
        public int OwnerId { get; private set; }
        public int LobbyId { get; private set; }

        public Lobby(int ownerId, int lobbyId)
        {
            OwnerId = ownerId;
            LobbyId = lobbyId;
        }

        public IEnumerable<PlayerServer> GetPlayers()
        {
            return _players;
        }

        public void EnterLobby(PlayerServer player)
        {
            if (_players.Contains(player))
                return;

            _players.Add(player);
        }

        public void ExitLobby(PlayerServer player)
        {
            if (!_players.Contains(player))
                return;

            _players.Remove(player);
        }
    }
}
