
using MessagePack;
using System.Collections.Generic;

namespace MsgPck
{
    [Union(1, typeof(LobbyServerMsg))]
    [Union(0, typeof(PlayerMsg))]
    public interface IUnionMsg
    { }

    [MessagePackObject]
    public class PlayerMsg
    {
        [Key(0)]
        public ulong Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
    }

    public enum ActionType
    {
        enter = 0,
        exit = 1,
        create = 2,
        update = 3,
        kick = 4,
    }

    [MessagePackObject]
    public class LobbyClientMsg
    {
        [Key(0)]
        public ActionType playerAction;
        [Key(1)]
        public ulong playerSubjectId;
        [Key(2)]
        public uint lobbyId;
    }

    [MessagePackObject]
    public class LobbyServerMsg
    {
        [Key(0)]
        public ulong ownerId;
        [Key(1)]
        public uint lobbyId;
        [Key(2)]
        public List<PlayerMsg> playerList;

    }
}
