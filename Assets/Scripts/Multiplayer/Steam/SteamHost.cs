using Steamworks;
using System;
using UnityEngine;

namespace Ulf
{
    public class SteamHost : SteamBase
    {
        private CallResult<LobbyCreated_t> lobbyCreated_t;
        private CallResult<LobbyEnter_t> lobbyEnter_t;

        private void Start()
        {
            lobbyCreated_t = CallResult<LobbyCreated_t>.Create(LobbyCreated);
            lobbyEnter_t = CallResult<LobbyEnter_t>.Create(LobbyEnter);
            CreateLobby();
        }

        private void LobbyEnter(LobbyEnter_t param, bool bIOFailure)
        {
            Debug.Log("Enter: " + param.m_ulSteamIDLobby);
        }

        void CreateLobby()
        {
            SteamAPICall_t handle = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeInvisible, 2);
            lobbyCreated_t.Set(handle);
            Debug.Log("Called Create Lobby()");
        }

        private void LobbyCreated(LobbyCreated_t param, bool bIOFailure)
        {
            Debug.Log(param.m_eResult);
            Debug.Log(param.m_ulSteamIDLobby);
            SteamMatchmaking.SetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name", pchName);
        }
    }
}