using Steamworks;
using System;
using Unity.Services.Lobbies;
using UnityEngine;

namespace Ulf
{
    public class SteamHost : SteamBase
    {
        private CallResult<LobbyCreated_t> lobbyCreated_t;
        private CallResult<LobbyPlayerJoined> lobbyJoined_t;
        private Callback<P2PSessionRequest_t> p2pSessionRequest_t;
        private Callback<LobbyEnter_t> lobbyEnter_t;

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

        }

        private void OnLobbyEnter(LobbyEnter_t param)
        {
            Debug.Log("Enter: " + param.m_ulSteamIDLobby);
            var ownerId = SteamMatchmaking.GetLobbyOwner((CSteamID)param.m_ulSteamIDLobby);
            //ConnectToHost(ownerId);
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
        }
    }
}