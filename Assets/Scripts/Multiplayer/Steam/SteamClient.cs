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
                GetHostConnect((CSteamID)param.m_ulSteamIDLobby);
            }
        }

        private void GetHostConnect(CSteamID lobbySteamID)
        {
            if(SteamMatchmaking.GetLobbyGameServer(lobbySteamID, out uint serverIp, out ushort serverPort, out CSteamID steamServerId))
            {
                Debug.Log("Success when try get server info!");

            }
            else
            {
                Debug.LogError("Error when try get server info");
            }

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