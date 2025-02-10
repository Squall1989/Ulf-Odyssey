using MessagePack;
using MsgPck;
using Steamworks;
using System;
using UlfServer;
using UnityEngine;

namespace Ulf
{
    public class SteamClient : SteamBase, INetworkable
    {
        private Callback<LobbyEnter_t> lobbyEnter_t;
        private Callback<LobbyChatUpdate_t> Callback_lobbyJoin;
        private CallResult<LobbyMatchList_t> lobbyMatchList_t;
        protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

        private CSteamID _hostId;

        public bool IsConnected => _hostId != default;


        private void Start()
        {
            Callback_lobbyJoin = Callback<LobbyChatUpdate_t>.Create(OnLobbyJoin);
            lobbyEnter_t = Callback<LobbyEnter_t>.Create(OnLobbyEnter);

            lobbyMatchList_t = CallResult<LobbyMatchList_t>.Create(OnLobbyList);
            Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);

            GetLobbies();
        }

        protected override void OnLobbyEnter(LobbyEnter_t param)
        {
            lobbySteamID = (CSteamID)param.m_ulSteamIDLobby;
            base.OnLobbyEnter(param);
            ConnectToHost(SteamMatchmaking.GetLobbyOwner((CSteamID)param.m_ulSteamIDLobby));

        }

        private void OnLobbyJoin(LobbyChatUpdate_t param)
        {
            //lobbySteamID = (CSteamID)param.m_ulSteamIDLobby;
        }
        bool isJoinOnce = false;
        private void OnGetLobbyInfo(LobbyDataUpdate_t param)
        {
            if (param.m_bSuccess == 0 || isJoinOnce)
                return;
            string gettingTitle_ = SteamMatchmaking.GetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name");

            if(gettingTitle_.Equals(pchName))
            {
                string code = SteamMatchmaking.GetLobbyData((CSteamID)param.m_ulSteamIDLobby, "code");
                if (code.Equals(pchCode))
                {
                    isJoinOnce = true;
                    SteamMatchmaking.JoinLobby((CSteamID)param.m_ulSteamIDLobby);
                }
            }
        }

        private void ConnectToHost(CSteamID hostId)
        {
            _hostId = hostId;

        }

        private void OnLobbyList(LobbyMatchList_t param, bool bIOFailure)
        {
            Debug.Log("Lobbies " + param.m_nLobbiesMatching);

            for (int i = 0; i < param.m_nLobbiesMatching; i++)
            {
                CSteamID lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
                var lobbyData = SteamMatchmaking.RequestLobbyData(lobbyId);
            }

        }



        private void GetLobbies()
        {
            SteamAPICall_t handle = SteamMatchmaking.RequestLobbyList();
            lobbyMatchList_t.Set(handle);
            Debug.Log("Called Get Lobbies()");
        }

        public void Send<T>(T message) where T : IUnionMsg
        {
            var bytes = Reader.Serialize<IUnionMsg>(message);
            SteamNetworking.SendP2PPacket(_hostId, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendUnreliableNoDelay);
        }
    }
}