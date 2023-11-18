using System.Threading.Tasks;
using Ulf;

public interface ISceneProxy
{
    Task<SnapSceneStruct> GetSceneStruct();

}
