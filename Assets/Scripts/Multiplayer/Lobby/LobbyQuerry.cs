

using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyQuerry
{
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

}
