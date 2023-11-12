using Zenject;
using UnityEngine;
using static Zenject.CheatSheet;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;

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
                    Container.Bind<SinglePlayerGame>().FromNew().AsCached();
                    Container.Bind<IGame>().To<SinglePlayerGame>().AsCached();
                    break;
                case GameType.online:
                    SetupServices();
                    handlerConnect.OnHostStart += (ishost) => SetupHost();
                    handlerConnect.OnGetClientCode += SetupClient;
                    Container.Bind<IGame>().To<MultiplayerGame>().AsCached();

                    break;
            }

            
        }

        

        private async void SetupServices()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void SetupClient(string code)
        {
            var client = new ClientRelay();
            client.OnLog += (log) => Debug.Log(log);
            client.Join(code);
        }

        private void SetupHost()
        {
            HostRelay host = new HostRelay();
            host.OnLog += (log) => Debug.Log(log);
            host.StartAllocate();
            Container.Bind<HostRelay>().FromInstance(host).AsSingle();
        }

    }
}