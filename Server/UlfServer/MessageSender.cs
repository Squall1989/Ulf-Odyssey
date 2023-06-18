using ENet;
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UlfServer
{
    public class MessageSender
    {
        public void PreparePacket<T>(T message, Peer peer) where T : IUnionMsg
        {
            Packet packet = default(Packet);

            byte[] data = Reader.Serialize<IUnionMsg>(message);

            packet.Create(data);
            peer.Send(0, ref packet);
        }

        public void UpdateLobby(Lobby lobby)
        {
            var players = lobby.GetPlayers();
            var playerMsgList = players.Select(p => new PlayerMsg() { Name = p.Name, Id = p.Id }).ToList();
            LobbyServerMsg lobbyMsg = new LobbyServerMsg() { playerList = playerMsgList, isOwner = false };

            foreach(var p in players)
            {
                if (p.Id == lobby.OwnerId)
                    lobbyMsg.isOwner = true;

                PreparePacket(lobbyMsg, p.Peer);
            }
        }

        internal void SendPlayerId(PlayerServer playerServer)
        {
            PlayerMsg playerMsg = new PlayerMsg() { Id = playerServer.Id, Name = playerServer.Name };
            PreparePacket(playerMsg, playerServer.Peer);
        }
    }
}
