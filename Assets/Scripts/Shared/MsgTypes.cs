
using MessagePack;
using System.Collections.Generic;
using Ulf;

namespace MsgPck
{
    [Union(0, typeof(SceneSnapMsg))]
    [Union(1, typeof(PlayerReadyMsg))]
    public interface IUnionMsg
    { }

    [MessagePackObject]
    public class SceneSnapMsg : IUnionMsg
    {
        [Key(0)]
        public SnapSceneStruct sceneStruct { get; set; }
    }
    [MessagePackObject]
    public class PlayerReadyMsg : IUnionMsg
    {

    }

}
