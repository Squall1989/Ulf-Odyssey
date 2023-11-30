using UnityEngine;
using Steamworks;
using System;

public class Initialization : MonoBehaviour
{
    private Callback<LobbyMatchList_t> m_LobbyMatchListCallResult;

    private void Start()
    {
        SteamAPI.Init();
        Debug.Log("IsSteamRunning: " + SteamAPI.IsSteamRunning());
        Debug.Log("GetHSteamUser: " + SteamAPI.GetHSteamUser());

        m_LobbyMatchListCallResult = Callback<LobbyMatchList_t>.Create(OnLobbyMatchList);


    }

    private void OnLobbyMatchList(LobbyMatchList_t param)
    {

    }

    private void Update()
    {

    }
}
