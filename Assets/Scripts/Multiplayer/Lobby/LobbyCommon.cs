
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyCommon 
{
    protected Lobby currentLobby;

    protected async void Subscribe()
    {
        LobbyEventCallbacks lobbyCallback = new();
        await Lobbies.Instance.SubscribeToLobbyEventsAsync(currentLobby.Id, lobbyCallback);
    }
}
