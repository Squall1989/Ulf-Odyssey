using ENet;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyClientUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField JoinCodeInput;
    [SerializeField] private TMPro.TextMeshProUGUI logText;
    [SerializeField] private Button joinButton;

    ClientRelay clientRelay;

    private void Start()
    {
        joinButton.onClick.AddListener(() => QueryLobbies()) ;
    }

    private async void QueryLobbies()
    {
        LobbyClient lobbyQuerry = new();
        var querry = await lobbyQuerry.GetLobbies();

        querry.Results.ForEach(p => Debug.Log($"name: {p.Name} id: {p.Id}"));

        var LobbyEnum = querry.Results.GetEnumerator();
        LobbyEnum.MoveNext();
        var firstLobby = LobbyEnum.Current;

        lobbyQuerry.JoinLobby(firstLobby);
    }


}