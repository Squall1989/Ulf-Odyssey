
using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    public class MultiplayerGame : IGame
    {
        protected int controlledPlayerId;
        protected List<PlayerData> _players = new ();
        private bool _isHost;
        private INetworkable _networker;
        private ISceneProxy _sceneProxy;

        public MultiplayerGame(INetworkable networker, ISceneProxy sceneProxy, bool isHost)
        {
            _isHost = isHost;
            _networker = networker;
            _sceneProxy = sceneProxy;
            //sender.OnPlayerIdSet += RegisterOurPlayer;
        }

        public void RegisterPlayer(PlayerData playerData)
        {
            _players.Add(playerData);
            
        }

    }
}