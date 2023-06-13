
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UlfServer
{
    public class Lobbies
    {
        List<Lobby> lobbies;

        private uint nextLobbyId = 0;

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
            }
        }

        private void Create(PlayerServer player)
        {
            Lobby lobby = new Lobby(player.Id, nextLobbyId++);
            lobby.EnterLobby(player);
        }

        private void Enter(PlayerServer player, uint lobbyId)
        {
            Lobby lobby = lobbies.FirstOrDefault(p => p.LobbyId == lobbyId);
            if(lobby != null)
            {
                lobby.EnterLobby(player);
            }
            else
            {
                Console.WriteLine("ERROR enter lobby(wrong id): " + lobbyId);
            }

        }
    }
}
