

using MsgPck;
using System;
using System.Threading.Tasks;
using Ulf;
/// <summary>
/// Connect to scene hoster
/// </summary>
public class SceneClient : ISceneProxy
{
    private INetworkable _networkable;

    public SceneClient(INetworkable networkable)
    {
        _networkable = networkable;
    }

    public Task<SnapSceneStruct> GetSceneStruct()
    {
        _networkable.RegisterHandler<SceneSnapMsg>(SceneReceived);
        
        SceneSnapMsg result = null;

        while(result == null)
        {
            Task.Delay(100);
        }

        return Task.FromResult(result.sceneStruct);

        void SceneReceived(SceneSnapMsg msg)
        {
           result = msg;
        }
    }
}
