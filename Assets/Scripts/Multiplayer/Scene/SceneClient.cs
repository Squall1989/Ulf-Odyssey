

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
    private SnapSceneStruct currScene;

    public SceneClient(INetworkable networkable, string playerId)
    {
        _playerId = playerId;
        _networkable = networkable;
        _networkable.RegisterHandler<SnapSceneStruct>(SceneReceived);
        
    }

    public async Task<SnapSceneStruct> GetSceneStruct()
    {

        while(!_networkable.IsConnected)
        {
            await Task.Yield();
        }

        _networkable.Send<IUnionMsg>(new PlayerData()
        {
            isReady = true,
            playerId = _playerId,
        });

        while (currScene ==  null)
        {
            await Task.Yield();
        };

        return currScene;
    }

    void SceneReceived(SnapSceneStruct msg)
    {
        currScene = msg;
    }
}
