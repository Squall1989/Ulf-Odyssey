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
            
        }
    }
}
