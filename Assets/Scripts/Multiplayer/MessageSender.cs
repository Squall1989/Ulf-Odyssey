
using ENet;
using MessagePack;
using MsgPck;
using System;
using System.Collections.Generic;
using System.IO;
using UlfServer;
using Zenject;

namespace Ulf
{
    public class MessageSender
    {
        //EnetConnect _connect;

        //public Action<List<PlayerMsg>> OnLobbyUpdate;
        //public Action<int, string> OnPlayerIdSet;

        //public MessageSender(EnetConnect injectConnect)
        //{
        //    _connect = injectConnect;
        //}

        //public void Init()
        //{
        //    _connect.InitConnect();
        //    //_connect.OnPacket += PacketRead;
        //    _connect.OnConnect += () => SendName("Maxim");
        //}

        //public void PreparePacket<T>(T message) where T : IUnionMsg
        //{
        //    Packet packet = default(Packet);

        //    byte[] data = Reader.Serialize<IUnionMsg>(message);

        //    packet.Create(data);
        //    _connect.Send(0, ref packet);
        //}

        //public void SendName(string name)
        //{
        //    var playerMsg = new PlayerMsg() { Name = name };
        //    PreparePacket(playerMsg);
        //}

        //public void CreateLobby()
        //{
        //    var lobbyMsg = new LobbyClientMsg() { playerAction = ActionType.create };
        //    PreparePacket(lobbyMsg);
        //}

        public T Deserialize<T>(byte[] bytes)
        {
            MessagePackReader reader = new MessagePackReader(bytes);
            var msg = MessagePackSerializer.Deserialize<T>(ref reader);
            return msg;
        }
    }
}