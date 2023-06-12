using Zenject;
using UnityEngine;

namespace Ulf
{
    public class GameInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            var options = new GameOptions();
            Container.Bind<GameOptions>().FromInstance(options).AsSingle();
            options.OnGameTypeChange += SetGameType;
        }


        public void SetGameType(GameType gameType)
        {
            if (gameType == GameType.single)
            {
                var NPCBeh = new NPCBehaviours();
                Container.Bind<IRegister<Unit>>().FromInstance(NPCBeh).AsCached();
            }
        }
    }
}