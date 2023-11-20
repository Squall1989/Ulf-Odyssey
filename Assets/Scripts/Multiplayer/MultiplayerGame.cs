
using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    public class MultiplayerGame : IGame
    {
        private INetworkable _networker;
        private ISceneProxy _sceneProxy;

        public MultiplayerGame(INetworkable networker, ISceneProxy sceneProxy)
        {
            _networker = networker;
            _sceneProxy = sceneProxy;
            //sender.OnPlayerIdSet += RegisterOurPlayer;
            SetupHandlers();
        }


        protected void SetupHandlers()
        {

        }


    }
}