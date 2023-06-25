using UnityEngine;
using Zenject;

namespace Ulf
{
    public class MultiplayerContext : MonoInstaller
    {
        [SerializeField] private EnetConnect connect;
        
        public override void InstallBindings()
        {
            Container.Bind<EnetConnect>().FromComponentInNewPrefab(connect).AsSingle().Lazy();
            Container.Bind<MessageSender>().FromNew().AsSingle();
        }
    }
}