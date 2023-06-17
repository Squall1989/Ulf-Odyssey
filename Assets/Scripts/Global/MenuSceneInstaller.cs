
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private EnetConnect connect;

    public override void InstallBindings()
    {
        Container.Bind<EnetConnect>().FromComponentInNewPrefab(connect).AsSingle();
    }
}
