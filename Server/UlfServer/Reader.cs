using MessagePack;
using System.IO;
using MsgPck;
using Event = ENet.Event;

namespace UlfServer
{
    internal class Reader
    {
        public static byte[] Serialize<T>(T thisObj) where T : IUnionMsg
        {
            using (var byteStream = new MemoryStream())
            {
                MessagePackSerializer.Serialize(byteStream, thisObj);
                return byteStream.ToArray();
            }
        }
        public static T Deserialize<T>(ref Event netEvent) where T : IUnionMsg
        {
            byte[] buffer = new byte[1024];

            netEvent.Packet.CopyTo(buffer);

            using (var byteStream = new MemoryStream(buffer))
            {
                return MessagePackSerializer.Deserialize<T>(byteStream);
            }
        }
    }
}
