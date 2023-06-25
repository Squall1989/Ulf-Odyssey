
using ENet;
using MsgPck;
using System;
using System.Collections.Generic;
using UlfServer;
using Zenject;

namespace Ulf
{
    public class MessageSender
    {
        LazyInject<EnetConnect> injectConnect;
        EnetConnect connect;

        public Action<List<PlayerMsg>> OnLobbyUpdate;
        public Action<int, string> OnPlayerIdSet;

        public MessageSender(LazyInject<EnetConnect> injectConnect)
        {
            this.injectConnect = injectConnect;
        }

        public void Init()
        {
            connect = injectConnect.Value;
            connect.InitConnect();
            connect.OnPacket += PacketRead;
            connect.OnConnect += () => SendName("Maxim");
        }

        public void PreparePacket<T>(T message) where T : IUnionMsg
        {
            Packet packet = default(Packet);

            byte[] data = Reader.Serialize<IUnionMsg>(message);

            packet.Create(data);
            connect.Send(0, ref packet);
        }

        public void SendName(string name)
        {
            var playerMsg = new PlayerMsg() { Name = name };
            PreparePacket(playerMsg);
        }

        public void CreateLobby()
        {
            var lobbyMsg = new LobbyClientMsg() { playerAction = ActionType.create };
            PreparePacket(lobbyMsg);
        }

        private void PacketRead(Packet packet)
        {
            var readData = Reader.Deserialize<IUnionMsg>(packet);
            switch (readData)
            {
                case LobbyServerMsg x:
                    OnLobbyUpdate?.Invoke(x.playerList);
                    break;
                case PlayerMsg x:
                    OnPlayerIdSet?.Invoke(x.Id, x.Name);
                    break;
            }

            packet.Dispose();

        }
    }
}