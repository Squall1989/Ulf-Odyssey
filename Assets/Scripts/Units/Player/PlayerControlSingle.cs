using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlSingle : PlayerControlBase, IPlayersProxy
    {
        private SceneGenerator _sceneGenerator;

        public PlayerControlSingle(SceneGenerator sceneGenerator, StatsScriptable[] stats) 
            : base(stats) 
        {
            _sceneGenerator = sceneGenerator;
        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            return Task.FromResult(_sceneGenerator.SpawnPlayer());
        }

        public override void AddPlayer(Player player, bool isOurPlayer)
        {
            base.AddPlayer(player, isOurPlayer);
            player.Actions.OnAttacked += CreateDamage;
        }
    }
}