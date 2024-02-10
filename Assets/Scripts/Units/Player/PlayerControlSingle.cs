using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlSingle : PlayerControlBase, IPlayerProxy
    {
        public PlayerControlSingle(SceneGenerator sceneGenerator) : base(sceneGenerator) 
        {
            
        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            return Task.FromResult(_sceneGenerator.SpawnPlayer());
        }
    }
}