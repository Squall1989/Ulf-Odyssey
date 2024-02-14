
using MessagePack;
using System.Collections.Generic;
using Ulf;

namespace MsgPck
{
    [Union(0, typeof(SnapSceneStruct))]
    [Union(1, typeof(PlayerData))]
    [Union(2, typeof(ActionData))]
    [Union(3, typeof(SnapPlayerStruct))]
    [Union(4, typeof(RequestPlayerSpawn))]
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
    public struct RequestPlayerSpawn : IUnionMsg
    {   }

    [MessagePackObject]
    public struct PlayerData : IUnionMsg
    {
        [Key(0)]
        public string playerId;
        [Key(1)]
        public bool isReady;
    }
    [MessagePackObject]
    public struct ActionData : IUnionMsg
    {
        [Key(0)]
        public int guid;
        [Key(1)]
        public INextAction action;
        [Key(2)]
        public bool isPlayerAction;
    }
}
