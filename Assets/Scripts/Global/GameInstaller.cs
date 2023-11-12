using Zenject;
using UnityEngine;
using static Zenject.CheatSheet;
using System;

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
                    handlerConnect.OnHostStart += SetupConnect;
                    Container.Bind<IGame>().To<MultiplayerGame>().AsCached();

                    break;
            }

            
        }

        private void SetupConnect(bool isHhost)
        {
            if(isHhost)
            {
                SetupHost();
            }
            else
            {
                SetupClient();
            }
        }

        private void SetupClient()
        {
            var client = new ClientRelay();
            client.OnLog += (log) => Debug.Log(log);
            handlerConnect.OnGetClientCode += (code) => client.Join(code);
        }

        private void SetupHost()
        {
            var host = new HostRelay();
            host.OnLog += (log) => Debug.Log(log);
            host.StartAllocate();
            Container.Bind<HostRelay>().FromInstance(host).AsCached();
        }
    }
}