using MsgPck;
using Steamworks;
using System.Collections.Generic;
using UlfServer;

namespace Ulf
{
    public class SteamHost : SteamBase, INetworkable
    {
        private List<CSteamID> clientList = new();

        public bool IsConnected => clientList.Count > 0;

        protected override void Awake()
        {
            base.Awake();
            RegisterHandler<PlayerData>(OnPlayerData);
        }

        private void OnPlayerData(PlayerData data, IConnectWrapper connect)
        {
            if(!clientList.Contains(UnwrapConnection(connect)))
            {
                clientList.Add(UnwrapConnection(connect));
            }

        }

        private void Start()
        {

        }


        protected override void P2pRequested(P2PSessionRequest_t param)
        {
            base.P2pRequested(param);
            clientList.Add(param.m_steamIDRemote);

        }

        private void OnDestroy()
        {
            
        }

        public void Send<T>(T message) where T : IUnionMsg
        {
            var bytes = Reader.Serialize<IUnionMsg>(message);
            foreach (var client in clientList)
            {
                SteamNetworking.SendP2PPacket(client, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendUnreliableNoDelay);
            }
        }
    }
}