
using System;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyCommon 
{
    protected Lobby currentLobby;

    protected async void Subscribe()
    {
        LobbyEventCallbacks lobbyCallback = new();
        lobbyCallback.LobbyChanged += LobbyChanged;
        await Lobbies.Instance.SubscribeToLobbyEventsAsync(currentLobby.Id, lobbyCallback);
    }

    private void LobbyChanged(ILobbyChanges changes)
    {

    }
}
