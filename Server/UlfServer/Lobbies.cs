
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UlfServer
{
    public class Lobbies
    {
        List<Lobby> lobbies;

        private int nextLobbyId = 0;

        public Action<Lobby> OnLobbyUpdate;

        public Lobbies()
        {
            lobbies = new List<Lobby>();
        }


        public void PlayerAction(LobbyClientMsg lobbyMsg, PlayerServer player)
        {
            switch(lobbyMsg.playerAction)
            {
                case ActionType.create:
                    Create(player);
                    break;
                case ActionType.enter:
                    Enter(player, lobbyMsg.lobbyId);
                    break;
                default: return;
            }

        }

        private void Create(PlayerServer player)
        {
            Lobby lobby = new Lobby(player.Id, nextLobbyId++);
            lobby.EnterLobby(player);
            OnLobbyUpdate?.Invoke(lobby);

        }

        private void Enter(PlayerServer player, int lobbyId)
        {
            Lobby lobby = lobbies.FirstOrDefault(p => p.LobbyId == lobbyId);
            if(lobby != null)
            {
                lobby.EnterLobby(player);
                OnLobbyUpdate?.Invoke(lobby);
            }
            else
            {
                Console.WriteLine("ERROR enter lobby(wrong id): " + lobbyId);
            }

        }
    }
}
