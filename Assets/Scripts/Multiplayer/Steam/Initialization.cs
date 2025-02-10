using UnityEngine;
using Steamworks;
using System;
using System.Threading.Tasks;
using Unity.Entities.UniversalDelegates;

public class Initialization : MonoBehaviour
{
    private CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;
    private CallResult<LobbyCreated_t> lobbyCreated_t;
    private CallResult<LobbyMatchList_t> lobbyMatchList_t;
    protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

    private const string pchName = "Ulf";

    private void Start()
    {
        SteamAPI.Init();
        if (SteamAPI.IsSteamRunning())
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
            m_NumberOfCurrentPlayers = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
            lobbyCreated_t = CallResult<LobbyCreated_t>.Create(LobbyCreated);
            lobbyMatchList_t = CallResult<LobbyMatchList_t>.Create(OnLobbyList);
            Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);
            GetNumPlayers();
            CreateLobby();
        }
    }

    private void OnGetLobbyInfo(LobbyDataUpdate_t param)
    {
        string gettingTitle_ = SteamMatchmaking.GetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name");
        Debug.Log(gettingTitle_);
    }

    private void OnLobbyList(LobbyMatchList_t param, bool bIOFailure)
    {
        Debug.Log("Lobbies " + param.m_nLobbiesMatching);

        for (int i = 0; i < param.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
            var lobbyData = SteamMatchmaking.RequestLobbyData(lobbyId);
            //Debug.Log("lobbyData :" + lobbyData);
        }
         
    }

    private void LobbyCreated(LobbyCreated_t param, bool bIOFailure)
    {
        Debug.Log(param.m_eResult);
        Debug.Log(param.m_ulSteamIDLobby);
        SteamMatchmaking.SetLobbyData((CSteamID)param.m_ulSteamIDLobby, "name", pchName);
        
    }


    private void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t param, bool bIOFailure)
    {
        Debug.Log(param.m_cPlayers);
    }

    private void OnLobbyMatchList(LobbyMatchList_t param)
    {

    }

    void GetLobbies()
    {
        SteamAPICall_t handle = SteamMatchmaking.RequestLobbyList();
        lobbyMatchList_t.Set(handle);
        Debug.Log("Called Get Lobbies()");
    }

    void CreateLobby()
    {
        SteamAPICall_t handle = SteamMatchmaking.CreateLobby( ELobbyType.k_ELobbyTypeInvisible, 2);
        lobbyCreated_t.Set(handle);
        Debug.Log("Called Create Lobby()");
    }

    void GetNumPlayers()
    {
        SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
        m_NumberOfCurrentPlayers.Set(handle);
        Debug.Log("Called GetNumberOfCurrentPlayers()");
    }

    private void Update()
    {
        SteamAPI.RunCallbacks();

    }
}
