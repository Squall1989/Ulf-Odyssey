using MsgPck;
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
            _networkable.RegisterHandler<ActionData>(OtherPlayerAction);
        }

        private void PlayerSpawnUnit(SnapPlayerStruct playerStruct)
        {
            OnOtherPlayerSpawn?.Invoke(playerStruct);
            _networkable.Send(playerStruct);
        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            var playerStruct = _sceneGenerator.SpawnPlayer();

            _networkable.Send(playerStruct);
            
            return Task.FromResult(playerStruct);
        }

        protected void OtherPlayerAction(ActionData playerActionData)
        {
            _networkable.Send(playerActionData);
            base.DoPlayerAction(playerActionData);
        }

        protected override void OurPlayerAction(ActionData playerActionData)
        {
            base.OurPlayerAction(playerActionData);
            _networkable.Send(playerActionData);
        }
    }
}