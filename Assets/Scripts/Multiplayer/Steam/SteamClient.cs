using MessagePack;
using MsgPck;
using Steamworks;
using System;
using UnityEngine;

namespace Ulf
{
    public class SteamClient : SteamBase
    {
        private Callback<LobbyChatUpdate_t> Callback_lobbyJoin;
        private CallResult<LobbyMatchList_t> lobbyMatchList_t;
        protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

        private void Start()
        {
            Callback_lobbyJoin = Callback<LobbyChatUpdate_t>.Create(OnLobbyJoin);

            lobbyMatchList_t = CallResult<LobbyMatchList_t>.Create(OnLobbyList);
            Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);

            GetLobbies();
        }

        private void OnLobbyJoin(LobbyChatUpdate_t param)
        {

        }

        bool isAlreadyJoined = false;
        private void OnGetLobbyInfo(LobbyDataUpdate_t param)
        {
            string gettingTitle_ = SteamMatchmaking.GetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name");
            Debug.Log(gettingTitle_);

            if(gettingTitle_.Equals(pchName) && !isAlreadyJoined)
            {
                isAlreadyJoined = true;
                SteamMatchmaking.JoinLobby((CSteamID)param.m_ulSteamIDLobby);
            }
        }

        private void ConnectToHost(CSteamID hostId)
        {
            var msg = MessagePackSerializer.Serialize<IUnionMsg>(new PlayerData()
            {
                 isReady = true,
                  playerId = "max",
            });

            var socket = SteamNetworking.SendP2PPacket(hostId, msg, (uint)msg.Length, EP2PSend.k_EP2PSendReliable);

        }

        private void OnLobbyList(LobbyMatchList_t param, bool bIOFailure)
        {
            Debug.Log("Lobbies " + param.m_nLobbiesMatching);

            for (int i = 0; i < param.m_nLobbiesMatching; i++)
            {
                CSteamID lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
                var lobbyData = SteamMatchmaking.RequestLobbyData(lobbyId);
                Debug.Log("lobbyData :" + lobbyData);
            }

        }



        private void GetLobbies()
        {
            SteamAPICall_t handle = SteamMatchmaking.RequestLobbyList();
            lobbyMatchList_t.Set(handle);
            Debug.Log("Called Get Lobbies()");
        }
    }
}