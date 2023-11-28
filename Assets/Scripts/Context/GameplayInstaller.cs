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
                        BindMultiplayerHost();
                    }
                    else
                    {
                        BindMultiplayerClient();
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
            Container.Bind<ISceneProxy>().To<SceneHost>().AsSingle();
            Container.Bind(typeof(IUnitsProxy), typeof(ITickable)).To<UnitsBehaviour>().FromNew().AsSingle();
        }

        private void BindMultiplayerHost()
        {
            Container.Bind<MultiplayerHost>().AsSingle().NonLazy();
        }

        private void BindMultiplayerClient()
        {
            Container.Bind<ISceneProxy>().To<SceneClient>().FromNew().AsCached();
            Container.Bind<UnitsController>().FromNew().AsSingle();
        }
    }
}
