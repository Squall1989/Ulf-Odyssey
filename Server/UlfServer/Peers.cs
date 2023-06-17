
using ENet;
using MsgPck;
using System;
using System.Collections.Generic;

namespace UlfServer
{
    public class Peers
    {
        private Dictionary<Peer, PlayerServer> peerList;
        private ulong nextPlayerId = 0;

        public Peers()
        {            
            peerList = new Dictionary<Peer, PlayerServer>();
        }

        public ulong NextPlayerId => nextPlayerId++;

        public void Add(Peer peer)
        {
            if(peerList.ContainsKey(peer))
            {
                Console.WriteLine("Peer already in peer list");
                return;
            }

            peerList.Add(peer, null);
            Console.WriteLine("Peer added, count: " + peerList.Count);

        }

        public void SetPlayer(Peer peer, PlayerMsg playerMsg)
        {
            PlayerServer player = new PlayerServer(playerMsg.Name, NextPlayerId, peer);

            if(peerList.ContainsKey(peer))
            {
                peerList[peer] = player;
            }
            else
            {
                peerList.Add(peer, player);
                Console.WriteLine("Player added, name: " + player.Name);
            }
        }

        public PlayerServer GetPlayer(Peer peer)
        {
            return peerList[peer];
        }

        public void Remove(Peer peer)
        {
            if(!peerList.ContainsKey(peer))
            {
                Console.WriteLine("Peer not present in peer list");
                return;
            }

            peerList.Remove(peer);
            Console.WriteLine("Peer removed, count: " + peerList.Count);
        }
    }

}