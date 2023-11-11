
using System.Threading.Tasks;

public interface IGame 
{
    public void RegisterPlayer(int id, string name);
    public Task<(int from, int limit)> GetPlanetsLimit();
}
