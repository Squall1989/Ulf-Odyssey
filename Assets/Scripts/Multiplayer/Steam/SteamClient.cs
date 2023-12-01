using MessagePack;
using MsgPck;
using Steamworks;
using UnityEngine;

namespace Ulf
{
    public class SteamClient : SteamBase
    {

        private CallResult<LobbyMatchList_t> lobbyMatchList_t;
        protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

        private void Start()
        {

            lobbyMatchList_t = CallResult<LobbyMatchList_t>.Create(OnLobbyList);
            Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);

            GetLobbies();
        }

        private void OnGetLobbyInfo(LobbyDataUpdate_t param)
        {
            string gettingTitle_ = SteamMatchmaking.GetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name");
            Debug.Log(gettingTitle_);

            if(gettingTitle_.Equals(pchName))
            {
                HostConnect(param);
            }
        }

        private void HostConnect(LobbyDataUpdate_t param)
        {
            var msg = MessagePackSerializer.Serialize<IUnionMsg>(new PlayerData()
            {
                 isReady = true,
                  playerId = "max",
            });

            SteamNetworking.SendP2PPacket((CSteamID)param.m_ulSteamIDMember, msg, (uint)msg.Length, EP2PSend.k_EP2PSendReliable);

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

        void GetLobbies()
        {
            SteamAPICall_t handle = SteamMatchmaking.RequestLobbyList();
            lobbyMatchList_t.Set(handle);
            Debug.Log("Called Get Lobbies()");
        }
    }
}