using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlSingle : PlayerControlBase, IPlayerProxy
    {
        private SceneGenerator _sceneGenerator;

        public PlayerControlSingle(SceneGenerator sceneGenerator) : base() 
        {
            _sceneGenerator = sceneGenerator;
        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            return Task.FromResult(_sceneGenerator.SpawnPlayer());
        }
    }
}