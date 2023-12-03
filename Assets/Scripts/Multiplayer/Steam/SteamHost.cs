using MsgPck;
using Steamworks;
using System;
using System.Collections.Generic;
using UlfServer;
using Unity.Services.Lobbies;
using UnityEngine;

namespace Ulf
{
    public class SteamHost : SteamBase, INetworkable
    {
        private CallResult<LobbyCreated_t> lobbyCreated_t;
        private CallResult<LobbyPlayerJoined> lobbyJoined_t;
        private Callback<P2PSessionRequest_t> p2pSessionRequest_t;
        private Callback<LobbyEnter_t> lobbyEnter_t;

        private List<CSteamID> clientList = new();
        public bool IsConnected => clientList.Count > 0;

        private void Start()
        {
            lobbyEnter_t = Callback<LobbyEnter_t>.Create(OnLobbyEnter);
            lobbyJoined_t = CallResult<LobbyPlayerJoined>.Create(OnLobbyPlayerJoined);
            lobbyCreated_t = CallResult<LobbyCreated_t>.Create(LobbyCreated);
            p2pSessionRequest_t = Callback<P2PSessionRequest_t>.Create(P2pRequested);
            CreateLobby();
        }


        private void OnLobbyPlayerJoined(LobbyPlayerJoined param, bool bio)
        {
            Debug.Log("Player joined: " + param.Player);

        }

        private void P2pRequested(P2PSessionRequest_t param)
        {
            if(SteamNetworking.AcceptP2PSessionWithUser(param.m_steamIDRemote))
                clientList.Add(param.m_steamIDRemote);
        }

        void CreateLobby()
        {
            SteamAPICall_t handle = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeInvisible, 2);
            lobbyCreated_t.Set(handle);
            Debug.Log("Called Create Lobby()");
        }

        private void LobbyCreated(LobbyCreated_t param, bool bio)
        {
            Debug.Log(param.m_eResult);
            Debug.Log(param.m_ulSteamIDLobby);
            SteamMatchmaking.SetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name", pchName);
            SteamMatchmaking.SetLobbyData((CSteamID)param.m_ulSteamIDLobby, "code", pchCode);
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