﻿using MessagePack;
using System.IO;
using MsgPck;
using Event = ENet.Event;
using System;
using ENet;

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
        public static T Deserialize<T>(Packet packet) where T : IUnionMsg
        {
            byte[] buffer = new byte[1024];

            packet.CopyTo(buffer);

            using (var byteStream = new MemoryStream(buffer))
            {
                return MessagePackSerializer.Deserialize<T>(byteStream);
            }
        }
    }
}