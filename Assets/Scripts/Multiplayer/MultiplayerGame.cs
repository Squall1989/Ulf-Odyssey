
using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    public class MultiplayerGame : IGame
    {
        protected (int id, string name) controlledPlayer;
        protected Dictionary<int, string> _players = new Dictionary<int, string>();

        MessageSender sender;

        [Inject] 
        public MultiplayerGame(MessageSender sender)
        {
            this.sender = sender;   
            sender.OnPlayerIdSet += RegisterOurPlayer;
        }

        public Task<(int from, int limit)> GetPlanetsLimit()
        {
            throw new NotImplementedException();
        }

        public void RegisterPlayer(int id, string name)
        {

            _players.Add(id, name);
            
        }

        private void RegisterOurPlayer(int id, string name)
        {
            controlledPlayer.name = name;
            controlledPlayer.id = id;
        }
    }
}