using System;
using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlHost : PlayerControlBase, IPlayerProxy
    {
        private INetworkable _networkable;

        public PlayerControlHost(SceneGenerator sceneGenerator, INetworkable networkable) : base(sceneGenerator)
        {
            _networkable = networkable;
            _networkable.RegisterHandler<SnapPlayerStruct>(PlayerSpawnUnit);

        }

        private void PlayerSpawnUnit(SnapPlayerStruct playerStruct)
        {

        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            var playerStruct = _sceneGenerator.SpawnPlayer();

            _networkable.Send(playerStruct);
            
            return Task.FromResult(playerStruct);
        }
    }
}