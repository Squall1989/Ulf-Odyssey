using Zenject;
using UnityEngine;
using static Zenject.CheatSheet;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

namespace Ulf
{
    public class GameInstaller : MonoInstaller
    {
        GameOptions options = new GameOptions();
        ConnectHandler handlerConnect = new ConnectHandler();

        public override void InstallBindings()
        {

            Container.Bind<GameOptions>().FromInstance(options).AsSingle();
            Container.Bind<ConnectHandler>().FromInstance(handlerConnect).AsSingle();
            options.OnGameTypeChange += SetGameType;

            Container.Bind<AllUnitsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
            Container.Bind<AllPlanetsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
        }





        public void SetGameType(GameType gameType)
        {
            switch (options.GameType)
            {
                case GameType.single:

                    break;
                case GameType.online:
                    SetupServices();
                    handlerConnect.OnHostStart += (ishost) => SetupHost();
                    handlerConnect.OnGetClientCode += SetupClient;

                    break;
            }

            
        }

        

        private async void SetupServices()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            string playerId = AuthenticationService.Instance.PlayerId;
            Container.Bind<string>().WithId("playerId").FromInstance(playerId).AsCached();
        }

        private void SetupClient(string code)
        {
            var client = new ClientRelay();
            client.OnLog += (log) => Debug.Log(log);
            client.Join(code);

            Container.Bind<INetworkable>().To<ClientRelay>().FromInstance(client).AsCached();

            SceneManager.LoadScene("GameScene");
        }

        private void SetupHost()
        {
            HostRelay host = new HostRelay();
            host.OnLog += (log) => Debug.Log(log);
            host.StartAllocate();
            Container.Bind<INetworkable>().To<HostRelay>().FromInstance(host).AsCached();
            Container.Bind<MultiplayerHost>().FromNew().AsCached();

            SceneManager.LoadScene("GameScene");

        }

    }
}