using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyCreate
{

    public async Task<Lobby> Create()
    {
        string lobbyName = "Max lobby";
        int maxPlayers = 4;
        CreateLobbyOptions options = new CreateLobbyOptions();
        options.IsPrivate = false;
        return await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
    }
}
