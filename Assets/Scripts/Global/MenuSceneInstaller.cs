
using Ulf;
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private EnetConnect connect;
    [SerializeField] private GameStarter starter;

    public override void InstallBindings()
    {
        Container.Bind<EnetConnect>().FromComponentInNewPrefab(connect).AsSingle();
        Container.Bind<GameStarter>().FromComponentInNewPrefab(starter).AsCached();
        Container.Bind<MessageSender>().FromNew().AsSingle();
    }
}
