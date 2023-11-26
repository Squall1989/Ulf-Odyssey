using MessagePack;
using MsgPck;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UlfServer;
using Unity.Collections;
using Unity.Networking.Transport;
using Unity.VisualScripting;

namespace Ulf
{
    public abstract class RelayBase
    {
        protected delegate void UnionConnectDelegate(IUnionMsg message, NetworkConnection connection);
        protected delegate void UnionDelegate(IUnionMsg message);
        protected Dictionary<Type, UnionConnectDelegate> callbacksConnectDict = new ();
        protected Dictionary<Type, UnionDelegate> callbacksDict = new ();
        public Action<string> OnLog;

        public virtual void RegisterHandler<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (callbacksDict.ContainsKey(type))
            {
                callbacksDict[type] += (msg) => callback((T)msg);
            }
            else
            {
                callbacksDict.Add(type, (msg) => callback((T)msg));
            }
        }

        public virtual void RegisterHandler<T>(Action<T, NetworkConnection> callback)
        {
            var type = typeof(T);
            if (callbacksDict.ContainsKey(type))
            {
                callbacksConnectDict[type] += (msg, connect) => callback((T)msg, connect);
            }
            else
            {
                callbacksConnectDict.Add(type, (msg, connect) => callback((T)msg, connect));
            }
        }

        public void Send<T>(T message) where T : IUnionMsg
        {
            NativeArray<byte> bytes = new NativeArray<byte>(Reader.Serialize<IUnionMsg>(message), Allocator.Temp);
            SendToAll(bytes);
        }

        public void Send<T>(T message, NetworkConnection connection) where T : IUnionMsg
        {
            NativeArray<byte> bytes = new NativeArray<byte>(Reader.Serialize<IUnionMsg>(message), Allocator.Temp);
            SendTo(bytes, connection);
        }

        protected void Read(DataStreamReader stream, NetworkConnection connection)
        {
            NativeArray<byte> bytes = new NativeArray<byte>(stream.Length, Allocator.Temp);
            stream.ReadBytes(bytes);

            var msg = MessagePackSerializer.Deserialize<IUnionMsg>(bytes.ToArray());
            OnLog?.Invoke("Message type: " + msg.GetType());
            callbacksConnectDict[msg.GetType()]?.Invoke(msg, connection);
        }
        protected void Read(DataStreamReader stream)
        {
            NativeArray<byte> bytes = new NativeArray<byte>(stream.Length, Allocator.Temp);
            stream.ReadBytes(bytes);

            var msg = MessagePackSerializer.Deserialize<IUnionMsg>(bytes.ToArray());

            callbacksDict[msg.GetType()]?.Invoke(msg);
        }

        protected abstract void SendToAll(NativeArray<byte> bytes);
        protected abstract void SendTo(NativeArray<byte> bytes, NetworkConnection connection);
    }
}