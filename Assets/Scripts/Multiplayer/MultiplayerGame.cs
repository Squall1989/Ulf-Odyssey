
using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    public class MultiplayerGame : IGame
    {
        private MultiplayerHost _multiplayerHost;
        private ISceneProxy _sceneProxy;


        public MultiplayerGame(ISceneProxy sceneProxy, MultiplayerHost multiplayerHost)
        {
            _multiplayerHost = multiplayerHost;
            _sceneProxy = sceneProxy;



            SetupHandlers();
        }


        protected void SetupHandlers()
        {

        }


    }
}