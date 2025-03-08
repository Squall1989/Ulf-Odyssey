using MsgPck;
using Steamworks;
using System;
using System.Text;
using UlfServer;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Ulf
{

    public class SteamClient : SteamBase, INetworkable
    {
        protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

        private CSteamID _hostId;
        private HSteamNetConnection connection;

        public bool IsConnected => _hostId != default;

        private void ConnectToHost(CSteamID hostId)
        {
            _hostId = hostId;
            SteamNetworkingIdentity steamNetworking = new SteamNetworkingIdentity();
            steamNetworking.SetSteamID(hostId);
            connection = SteamNetworkingSockets.ConnectP2P(ref steamNetworking, 0, 0, new SteamNetworkingConfigValue_t[0]);
            UnityEngine.Debug.Log("Connected to server");
        }


        public void Send<T>(T message) where T : IUnionMsg
        {
            var bytes = Reader.Serialize<IUnionMsg>(message);
            SteamNetworking.SendP2PPacket(_hostId, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendUnreliableNoDelay);
        }

        internal void Connect(string name, ulong id)
        {
            ConnectToHost((CSteamID)id);
        }
    }
}