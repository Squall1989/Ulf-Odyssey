
using System.Collections.Generic;

namespace UlfServer
{
    public class Lobby
    {
        List<PlayerServer> _players = new List<PlayerServer>();
        
        public ulong OwnerId { get; private set; }
        public uint LobbyId { get; private set; }

        public Lobby(ulong ownerId, uint lobbyId)
        {
            OwnerId = ownerId;
            LobbyId = lobbyId;
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
