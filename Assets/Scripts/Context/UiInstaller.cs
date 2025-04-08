using UnityEngine;
using Zenject;

namespace Ulf
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private EnemyHpUi hpUiPfb;
        [SerializeField] private EnemyHpUiWorld hpWorldUiPfb;
        [SerializeField] private ViewUIController viewUI;
        public override void InstallBindings()
        {
            var pieces = Resources.LoadAll<ElementPiecesScriptable>("Containers/Sprites/Pieces");
            Container.Bind<ElementPiecesScriptable[]>().FromInstance(pieces).AsSingle();

            Container.Bind<ElementSpritesScriptable>().FromScriptableObjectResource("Containers/Sprites").AsSingle();
            Container.Bind<EnemyHpUi>().FromComponentInNewPrefab(hpUiPfb).AsSingle();
            Container.Bind<EnemyHpUiWorld>().FromComponentInNewPrefab(hpWorldUiPfb).AsSingle();
            Container.Bind<ViewUIController>().FromInstance(viewUI).AsSingle();
        }
    }
}
