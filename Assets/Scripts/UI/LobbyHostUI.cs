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



    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        LobbyCreate lobbyHost = new();
        var lobby = await lobbyHost.Create();
        Debug.Log("lobby created: " + lobby.Name);

        
    }

}
