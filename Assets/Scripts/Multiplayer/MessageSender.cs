
using ENet;
using MsgPck;
using System;
using System.Collections.Generic;
using System.Drawing;
using UlfServer;

namespace Ulf
{
    public class MessageSender
    {
        private EnetConnect connect;

        public Action<List<PlayerMsg>> OnLobbyUpdate;
        public Action<int> OnPlayerIdSet;

        public MessageSender(EnetConnect connect)
        {
            this.connect = connect;
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
                    OnPlayerIdSet?.Invoke(x.Id);
                    break;
            }

            packet.Dispose();

        }
    }
}