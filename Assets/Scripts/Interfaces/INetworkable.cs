using MsgPck;
using System;

public interface INetworkable
{
    void Send(string message);
    void RegisterHandler<T>(Action<T> callback);

    Action<string> OnReceive { get; set; }
}
