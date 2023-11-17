using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class GameplayInstaller : MonoInstaller
    {
        [Inject] GameOptions options;
        [Inject] ConnectHandler handlerConnect;

        public override void InstallBindings()
        {
            SwitchGameMode();
        }

        protected void SwitchGameMode()
        {
            switch(options.GameType)
            {
                case GameType.online:

                    Container.Bind<bool>().FromInstance(handlerConnect.IsHost);

                    if (handlerConnect.IsHost)
                    {
                        BindHost();
                    }
                    else
                    {
                        BindClient();
                    }

                    Container.Bind<IGame>().To<MultiplayerGame>().FromNew().AsCached();

                    break;
                case GameType.single:
                    Container.Bind<IGame>().To<SinglePlayerGame>().FromNew().AsCached(); 
                    BindHost();
                    break;
            }
        }

        private void BindHost()
        {
            Container.Bind<SceneGenerator>().FromNew().AsSingle();
            Container.Bind<ISceneProxy>().To<SceneHost>().FromNew().AsCached();
        }

        private void BindClient()
        {
            Container.Bind<ISceneProxy>().To<SceneClient>().FromNew().AsCached();
        }
    }
}
