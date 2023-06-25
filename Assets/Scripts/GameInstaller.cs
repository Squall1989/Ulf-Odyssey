using Zenject;
using UnityEngine;

namespace Ulf
{
    public class GameInstaller : MonoInstaller
    {
        private IGame game = default(IGame);
        GameOptions options = new GameOptions();

        public override void InstallBindings()
        {

            Container.Bind<GameOptions>().FromInstance(options).AsSingle();
            options.OnGameTypeChange += SetGameType;

            Container.Bind<IGame>().FromSubContainerResolve().ByMethod(GameInstall).AsSingle();
        }

        private void GameInstall(DiContainer subContainer)
        {
            switch (options.GameType)
            {
                case GameType.single:
                    Container.Bind<MultiplayerGame>().FromNew().AsSingle();
                    Container.Bind<IGame>().To<MultiplayerGame>().AsSingle();
                    break;
                case GameType.online:
                    Container.Bind<IGame>().To<MultiplayerGame>().AsCached();
                    break;
            }
        }



        public void SetGameType(GameType gameType)
        {
            if (gameType == GameType.single)
            {

            }
            else if(gameType == GameType.online)
            {

            }
        }
    }
}