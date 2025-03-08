
using System;

public class ConnectHandler 
{
    public Action<bool> OnHostStart;
    public Action<string, ulong> OnClientConnect;

    public bool IsHost { get; private set; }

    public void SetMode(bool isHots)
    {
        IsHost = isHots;
        OnHostStart?.Invoke(isHots);
    }

    public void ClientConnect(string name, ulong id)
    {
        OnClientConnect?.Invoke(name, id);
    }
}
