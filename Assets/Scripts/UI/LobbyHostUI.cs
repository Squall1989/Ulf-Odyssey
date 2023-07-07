using System;
using System.Collections;
using System.Collections.Generic;
using Ulf;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHostUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI codeText;
    [SerializeField] private Button startHostButton;
    [SerializeField] private TMPro.TextMeshProUGUI logText;
    private HostRelay host;



    private void Start()
    {
        startHostButton.onClick.AddListener(() => HostLobbyStart());

        
    }

    private async void HostLobbyStart()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        LobbyCreate lobbyHost = new();
        var lobby = await lobbyHost.Create();
        Debug.Log("lobby created: " + lobby.Name);
    }

}
