using MessagePack;
using System.IO;
using MsgPck;
namespace UlfServer
{
    internal class Reader
    {

        public static byte[] Serialize<T>(T thisObj) 
        {
            byte[] bytes = MessagePackSerializer.Serialize(thisObj);

            return bytes;
        }
        public static T Deserialize<T>(byte[] buffer)
        {
            return MessagePackSerializer.Deserialize<T>(buffer);
            
        }
    }
}
