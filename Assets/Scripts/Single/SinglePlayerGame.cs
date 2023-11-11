
using System.Threading.Tasks;

public class SinglePlayerGame : IGame
{
    public Task<(int from, int limit)> GetPlanetsLimit()
    {
        int limit = UnityEngine.Random.Range(3, 10);

        return Task.FromResult((0, limit));
    }

    public void RegisterPlayer(int id, string name)
    {

    }
}
