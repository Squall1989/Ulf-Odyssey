using System.Threading.Tasks;
using ENet;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyCreate : LobbyCommon
{
    public async Task<Lobby> Create()
    {
        string lobbyName = "Max lobby";
        int maxPlayers = 4;
        CreateLobbyOptions options = new CreateLobbyOptions();
        options.IsPrivate = false;
        currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
        Subscribe();

        return currentLobby;
    }
}
