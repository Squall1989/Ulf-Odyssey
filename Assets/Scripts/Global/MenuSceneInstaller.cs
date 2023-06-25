
using Ulf;
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] LobbyControl panelLobby;

    public override void InstallBindings()
    {
        Container.Bind<LobbyControl>().FromComponentInNewPrefab(panelLobby).AsCached();
    }
}
