using MsgPck;
using System;
using Unity.Networking.Transport;

public interface INetworkable
{
    void Send<T>(T message) where T : IUnionMsg;
    void Send<T>(T message, NetworkConnection connection) where T : IUnionMsg;
    void RegisterHandler<T>(Action<T> callback);
    void RegisterHandler<T>(Action<T, NetworkConnection> callback);

    Action<string> OnReceive { get; set; }
}
