using Zenject;
using UnityEngine;
using static Zenject.CheatSheet;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Ulf
{
    public class GameInstaller : MonoInstaller
    {
        GameOptions options = new GameOptions();
        ConnectHandler handlerConnect = new ConnectHandler();

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;

            Container.Bind<GameOptions>().FromInstance(options).AsSingle();
            Container.Bind<ConnectHandler>().FromInstance(handlerConnect).AsSingle();
            options.OnGameTypeChange += SetGameType;

            Container.Bind<AllUnitsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
            Container.Bind<AllPlanetsScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
            Container.Bind<AllBuildScriptable>().FromScriptableObjectResource("Containers/").AsSingle();
        }





        public void SetGameType(GameType gameType)
        {
            switch (options.GameType)
            {
                case GameType.single:

                    break;
                case GameType.online:
                    SetupServices();
                    handlerConnect.OnHostStart += (ishost) => SetupSteamHost();
                    handlerConnect.OnGetClientCode += (code) => SetupSteamClient();

                    break;
            }

            
        }

        

        private async void SetupServices()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            string playerId = AuthenticationService.Instance.PlayerId;
            Container.Bind<string>().FromInstance(playerId).AsSingle();
        }

        protected void SetupSteamClient()
        {
            Container.Rebind<INetworkable>().To<SteamClient>().FromComponentInNewPrefabResource("Steam/SteamClient").AsSingle();

            SceneManager.LoadScene("GameScene");
        }

        protected void SetupSteamHost()
        {
            Container.Rebind<INetworkable>().To<SteamHost>().FromComponentInNewPrefabResource("Steam/SteamHost").AsSingle();

            SceneManager.LoadScene("GameScene");
        }

        private void SetupUnityClient(string code)
        {
            var client = new ClientRelay();
            client.OnLog += (log) => Debug.Log(log);
            client.Join(code);
            client.OnJoined += () => SceneManager.LoadScene("GameScene");
            
            Container.Rebind<INetworkable>().To<ClientRelay>().FromInstance(client).AsSingle();
        }

        private void SetupUnityHost()
        {
            HostRelay host = new HostRelay();
            host.OnLog += (log) => Debug.Log(log);
            host.StartAllocate();
            Container.Bind<INetworkable>().To<HostRelay>().FromInstance(host).AsSingle();

            SceneManager.LoadScene("GameScene");

        }

    }
}