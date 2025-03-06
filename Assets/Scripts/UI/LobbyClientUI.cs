using ENet;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyClientUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI joinText;
    [SerializeField] private Button joinButton;
    [Inject] ConnectHandler connectHandler;
    [Inject] IFriendConnectable friendConnect;
    private void Awake()
    {
        joinButton.gameObject.SetActive(false);
        friendConnect.OnFriendInGame += FriendInGame;
    }

    private void Start()
    {
        
        friendConnect.RequestFriendsInGame();
    }

    private void FriendInGame(string name, ulong id)
    {
        joinButton.gameObject.SetActive(true);
        joinText.text = "join to " +name;
        UnityEngine.Debug.Log(name + " in game");
        joinButton.onClick.RemoveAllListeners();
        joinButton.onClick.AddListener(() =>
        {
            connectHandler.ClientConnect(name, id);
        });
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
