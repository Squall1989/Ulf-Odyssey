using MsgPck;
using System;
using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlClient : PlayerControlBase, IPlayerProxy
    {
        private string _playerId;
        private INetworkable _networkable;

        public PlayerControlClient(INetworkable networkable, string playerId) : base() 
        {
            _playerId = playerId;
            _networkable = networkable;
            _networkable.RegisterHandler<SnapPlayerStruct>(OtherPlayerSpawn);
            _networkable.RegisterHandler<ActionData>(OtherPlayerAction);
        }

        private void OtherPlayerAction(ActionData data)
        {
            if(!data.isPlayerAction)
            {
                return;
            }

            if(data.guid == _player.GUID)
            {
                return;
            }

            DoPlayerAction(data);
        }

        private void OtherPlayerSpawn(SnapPlayerStruct playerStruct)
        {
            if (playerStruct.playerId == _playerId)
                return;

            OnOtherPlayerSpawn?.Invoke(playerStruct);
        }

        public async Task<SnapPlayerStruct> SpawnPlayer()
        {
            _networkable.Send(new RequestPlayerSpawn());

            SnapPlayerStruct snapPlayerStruct = default;

            _networkable.RegisterHandler<SnapPlayerStruct>(receiveStruct);

            while(string.IsNullOrEmpty(snapPlayerStruct.playerId))
            {
                await Task.Delay(300);
            }
            _networkable.UnRegisterHandler<SnapPlayerStruct>(receiveStruct); ;

            return snapPlayerStruct;

            void receiveStruct(SnapPlayerStruct playerStruct)
            {
                if (playerStruct.playerId == _playerId)
                    snapPlayerStruct = playerStruct;

            }
        }

        protected override void OurPlayerAction(ActionData playerActionData)
        {
            base.OurPlayerAction(playerActionData);
            _networkable.Send(playerActionData);
        }
    }
}