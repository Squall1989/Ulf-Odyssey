
using MessagePack;
using System.Collections.Generic;
using Ulf;

namespace MsgPck
{
    [Union(0, typeof(SnapSceneStruct))]
    [Union(1, typeof(PlayerData))]
    public interface IUnionMsg
    { }

    [MessagePackObject]
    public class SnapSceneStruct : IUnionMsg
    {
        [Key(0)]
        public int totalCount;
        [Key(1)]
        public int currentCount;
        [Key(2)]
        public SnapPlanetStruct snapPlanet;
    }
    [MessagePackObject]
    public struct PlayerData : IUnionMsg
    {
        [Key(0)]
        public string playerId;
        [Key(1)]
        public bool isReady;
    }

}
