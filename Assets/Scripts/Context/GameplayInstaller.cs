using Cinemachine;
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
        [SerializeField] private BridgeMono bridgeExample;
        [SerializeField] private InputControl input;
        [SerializeField] protected CinemachineVirtualCamera cameraControl;
        [SerializeField] protected PlayerMono playerMono;

        public override void InstallBindings()
        {
            SwitchGameMode();
            Container.Bind<PlayerMono>().FromInstance(playerMono).AsSingle();
            Container.Bind<InputControl>().FromComponentInNewPrefab(input).AsSingle();
            Container.Bind<CinemachineVirtualCamera>().FromInstance(cameraControl).AsSingle();
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
                    Container.Bind<IPlayersProxy>().To<PlayerControlSingle>().AsSingle();
                    BindHost();
                    break;
            }
        }

        private void BindHost()
        {
            Container.Bind<BridgeMono>().FromInstance(bridgeExample).AsSingle();
            Container.Bind<SceneGenerator>().FromNew().AsSingle();
            Container.Bind<ISceneProxy>().To<SceneHost>().AsSingle();
            Container.Bind(typeof(IUnitsProxy), typeof(ITickable)).To<UnitsDecisions>().FromNew().AsSingle();
        }

        private void BindMultiplayerHost()
        {
            Container.Bind<MultiplayerHost>().AsSingle().NonLazy();
            Container.Bind<IPlayersProxy>().To<PlayerControlHost>().AsSingle();
        }

        private void BindMultiplayerClient()
        {
            Container.Bind<ISceneProxy>().To<SceneClient>().FromNew().AsCached();
            Container.Bind<IUnitsProxy>().To<UnitsController>().FromNew().AsSingle();
            Container.Bind<IPlayersProxy>().To<PlayerControlClient>().AsSingle();
        }
    }
}
