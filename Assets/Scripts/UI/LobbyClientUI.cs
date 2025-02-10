using ENet;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyClientUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField JoinCodeInput;
    [SerializeField] private TMPro.TextMeshProUGUI logText;
    [SerializeField] private Button joinButton;
    [Inject] ConnectHandler connectHandler;
    //ClientRelay clientRelay;

    private void Start()
    {
        joinButton.onClick.AddListener(() =>
        {
            connectHandler.SetCode(JoinCodeInput.text);
        }) ;
    }

    private async void QueryLobbies()
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
        LobbyClient lobbyQuerry = new();
        var querry = await lobbyQuerry.GetLobbies();

        querry.Results.ForEach(p => Debug.Log($"name: {p.Name} id: {p.Id}"));

        var LobbyEnum = querry.Results.GetEnumerator();
        LobbyEnum.MoveNext();
        var firstLobby = LobbyEnum.Current;

        lobbyQuerry.JoinLobby(firstLobby);
        lobbyQuerry.SendMessage("Hello!!!!!!!");
    }


}
