
using System;

public class ConnectHandler 
{
    public Action<bool> OnHostStart;
    public Action<string> OnGetClientCode;

    public bool IsHost { get; private set; }

    public void SetMode(bool isHots)
    {
        IsHost = isHots;
        OnHostStart?.Invoke(isHots);
    }

    public void SetCode(string code)
    {
        OnGetClientCode?.Invoke(code);
    }
}
