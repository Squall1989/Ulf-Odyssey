using Zenject;
using UnityEngine;
using static Zenject.CheatSheet;

namespace Ulf
{
    public class GameInstaller : MonoInstaller
    {
        GameOptions options = new GameOptions();

        public override void InstallBindings()
        {

            Container.Bind<GameOptions>().FromInstance(options).AsSingle();
            options.OnGameTypeChange += SetGameType;

            Container.Bind<AllUnitsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
            Container.Bind<AllPlanetsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();

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