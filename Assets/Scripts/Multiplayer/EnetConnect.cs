using UnityEngine;
using ENet;
using System;
using EventType = ENet.EventType;
using Event = ENet.Event;
using System.Text;
using System.Threading.Tasks;
using UlfServer;
using MsgPck;

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

    public Action<Packet> OnPacket;
    public Action OnConnect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitConnect()
    {
        ENet.Library.Initialize();
        
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
                OnConnect?.Invoke();
                break;

            case EventType.Disconnect:
                Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                break;
            case EventType.Timeout:
                Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                break;
            case EventType.Receive:
                Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                OnPacket?.Invoke(netEvent.Packet);
                break;
        }
    }



    private void OnDestroy()
    {
        ENet.Library.Deinitialize();
    }

    public void Send(byte channel, ref Packet packet)
    {
        peer.Send(channel, ref packet);
    }
}
