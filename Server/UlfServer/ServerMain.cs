using ENet;
using MsgPck;
using System;

namespace UlfServer
{
    public class ServerMain
    {
        bool isOnline = true;
        Address address = new Address();
        private ushort port = 7777;
        private int maxClients = 4;

        Event netEvent;

        private Peers peers;
        private Lobbies lobbies;
        private Host host;

        private MessageSender messageSender = new MessageSender();

        public ServerMain()
        {
            Library.Initialize();
        }

        public void InitServer()
        {
            host = new Host();
            peers = new Peers();
            lobbies = new Lobbies();
            lobbies.OnLobbyUpdate += messageSender.UpdateLobby;
            address.Port = port;

            int channelCount = Enum.GetValues(typeof(ChannelType)).Length;
            host.Create(address, maxClients, channelCount);
        }

        internal void WaitPackage()
        {
            while (isOnline)
            {
                if( host.CheckEvents(out netEvent) <= 0 )
                {
                    if (host.Service(15, out netEvent) <= 0)
                        continue;
                }

                SwitchEvents();
            }

        }

        private void SwitchEvents()
        {

            switch (netEvent.Type)
            {
                case EventType.None:
                    break;

                case EventType.Connect:
                    Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    peers.Add(netEvent.Peer);
                    break;

                case EventType.Disconnect:
                    Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    peers.Remove(netEvent.Peer);
                    break;

                case EventType.Timeout:
                    Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;

                case EventType.Receive:
                    Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                    PacketRead(netEvent.Packet);
                    break;
            }
        }

        private void PacketRead(Packet packet)
        {
            var readData = Reader.Deserialize<IUnionMsg>(packet);
            switch (readData)
            {
                case LobbyClientMsg x:
                    var player = peers.GetPlayer(netEvent.Peer);
                    lobbies.PlayerAction(x, player);
                    break;
                case PlayerMsg x:
                    peers.SetPlayer(netEvent.Peer, x);
                    messageSender.SendPlayerId(peers.GetPlayer(netEvent.Peer));
                    break;
            }

            packet.Dispose();

        }
    }

    public enum ChannelType
    {
        ping = 0,
        mainGame = 1,
        lobby = 2,
    }
}