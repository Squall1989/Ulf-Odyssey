

using MsgPck;
using System;
using System.Threading.Tasks;
using Ulf;
/// <summary>
/// Connect to scene hoster
/// </summary>
public class SceneClient : ISceneProxy
{
    private string _playerId;
    private INetworkable _networkable;

    public SceneClient(INetworkable networkable, string playerId)
    {
        _playerId = playerId;
        _networkable = networkable;
    }

    public Task<SnapSceneStruct> GetSceneStruct()
    {
        _networkable.RegisterHandler<SnapSceneStruct>(SceneReceived);
        var playerData = new PlayerData()
        {
             isReady = true,
              playerId = _playerId,
        };
        _networkable.Send(playerData);

        SnapSceneStruct result = null;

        while(result == null)
        {
            Task.Delay(100);
        }

        return Task.FromResult(result);

        void SceneReceived(SnapSceneStruct msg)
        {
           result = msg;
        }
    }
}
