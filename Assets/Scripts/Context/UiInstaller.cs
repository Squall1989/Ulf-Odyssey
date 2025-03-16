using UnityEngine;
using Zenject;

namespace Ulf
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private EnemyHpUi hpUiPfb;
        public override void InstallBindings()
        {
            Container.Bind<ElementSpritesScriptable>().FromScriptableObjectResource("Containers/Sprites").AsSingle();
            Container.Bind<EnemyHpUi>().FromComponentInNewPrefab(hpUiPfb).AsSingle();
            Container.Bind<ViewController>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
