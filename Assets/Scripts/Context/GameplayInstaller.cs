using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class GameplayInstaller : MonoInstaller
    {
        [Inject] GameOptions options;

        public override void InstallBindings()
        {
            SwitchGameMode();
            Container.Bind<SceneGenerator>().FromNew().AsSingle();
        }

        protected void SwitchGameMode()
        {
            switch (options.GameType)
            {
                case GameType.single:
                    Container.Bind<SinglePlayerGame>().FromNew().AsCached();
                    Container.Bind<IGame>().To<SinglePlayerGame>().AsCached();
                    break;
                case GameType.online:
                    Container.Bind<IGame>().To<MultiplayerGame>().AsCached();
                    break;
            }
        }
    }
}
