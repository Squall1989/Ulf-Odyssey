
using MessagePack;
using System.Collections.Generic;

namespace MsgPck
{
    [Union(0, typeof(LobbyClientMsg))]
    [Union(1, typeof(LobbyServerMsg))]
    [Union(2, typeof(PlayerMsg))]
    public interface IUnionMsg
    { }

    [MessagePackObject]
    public class PlayerMsg : IUnionMsg
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public int Id { get; set; }
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
    public class LobbyClientMsg : IUnionMsg
    {
        [Key(0)]
        public ActionType playerAction;
        [Key(1)]
        public int lobbyId;
    }

    [MessagePackObject]
    public class LobbyServerMsg : IUnionMsg
    {
        [Key(0)]
        public List<PlayerMsg> playerList;
        [Key(1)]
        public bool isOwner;
    }
}
