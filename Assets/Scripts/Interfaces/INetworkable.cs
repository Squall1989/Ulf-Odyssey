using System;

public interface INetworkable
{
    void Send(string message);
    Action<string> OnReceive { get; set; }
}
