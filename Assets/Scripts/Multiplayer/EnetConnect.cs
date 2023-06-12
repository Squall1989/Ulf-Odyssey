using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENet;
using UnityEditor.PackageManager;
using System;
using EventType = ENet.EventType;
using Event = ENet.Event;
using System.Text;
using System.Threading.Tasks;

public class EnetConnect : MonoBehaviour
{
    [SerializeField]
    private string ip = "127.0.0.1";
    [SerializeField]
    private ushort port = 7777;

    Peer peer;

    Event netEvent;

    Host client = new Host();
    private bool isOnline = true;

    // Start is called before the first frame update
    void Start()
    {
        ENet.Library.Initialize();
        InitConnect();
    }

    private void InitConnect()
    {
        Address address = new Address();

        address.SetHost(ip);
        address.Port = port;
        client.Create();

        peer = client.Connect(address);

        WaitPackage();
    }

    internal async void WaitPackage()
    {
        while (isOnline)
        {
            await Task.Delay(200);
            if (client.CheckEvents(out netEvent) <= 0)
            {
                if (client.Service(15, out netEvent) <= 0)
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

                SendPacket();
                break;

            case EventType.Disconnect:
                Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                break;
            case EventType.Timeout:
                Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                break;
            case EventType.Receive:
                Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                break;
        }
    }

    private void SendPacket()
    {
        Packet packet = default(Packet);
        byte[] data = Encoding.UTF8.GetBytes("hello");

        packet.Create(data);
        peer.Send(0, ref packet);
        peer.Send(0, ref packet);
        peer.Send(0, ref packet);
        peer.Send(0, ref packet);
        peer.Send(0, ref packet);
    }

    private void OnDestroy()
    {
        ENet.Library.Deinitialize();
    }

}
