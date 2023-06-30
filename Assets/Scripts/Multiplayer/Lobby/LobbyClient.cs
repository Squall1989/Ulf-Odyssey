

using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyClient
{
    private Lobby currentLobby;

    public Task<QueryResponse> GetLobbies()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;


            // Order by newest lobbies first
            options.Order = new List<QueryOrder>()
            {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            return Lobbies.Instance.QueryLobbiesAsync(options);

            //...
            }
            catch (LobbyServiceException e)
            {
            throw new System.Exception("error: " + e);
            }
    }

    public async void JoinLobby(Lobby lobby)
    {
        try
        {

            await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id);
            currentLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            throw new System.Exception("lobby join exception: " + e);
        }
    }

    public async void SendMessage(string message)
    {
        string playerId = AuthenticationService.Instance.PlayerId;

        Dictionary<string, PlayerDataObject> dataCurr = new Dictionary<string, PlayerDataObject>();
        UpdatePlayerOptions updateOptions = new UpdatePlayerOptions
        {
            Data = dataCurr,
            AllocationId = null,
            ConnectionInfo = null
        };

         var dataObj = new PlayerDataObject(visibility: PlayerDataObject.VisibilityOptions.Member,
                    value:  "Hello");

        dataCurr.Add("message", dataObj);


        currentLobby = await LobbyService.Instance.UpdatePlayerAsync(currentLobby.Id, playerId, updateOptions);

    }
}
