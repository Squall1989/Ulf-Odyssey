using MsgPck;
using System;
using Unity.Networking.Transport;
using Ulf;

public interface INetworkable
{
    void Send<T>(T message) where T : IUnionMsg;
    void Send<T>(T message, IConnectWrapper connection) where T : IUnionMsg;
    void RegisterHandler<T>(Action<T> callback);
    void RegisterHandler<T>(Action<T, IConnectWrapper> callback);
    bool IsConnected {  get; }
    Action<string> OnReceive { get; set; }
}
