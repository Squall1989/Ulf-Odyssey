using MsgPck;
using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlClient : PlayerControlBase, IPlayerProxy
    {
        private INetworkable _networkable;

        public PlayerControlClient(INetworkable networkable, SceneGenerator sceneGenerator) : base(sceneGenerator) 
        {
            _networkable = networkable;
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
                snapPlayerStruct = playerStruct;

            }
        }
    }
}