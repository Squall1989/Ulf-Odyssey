
using System;

public class ConnectHandler 
{
    public Action<bool> OnHostStart;
    public Action<string> OnGetClientCode;
    public void SetMode(bool isHots)
    {
        OnHostStart?.Invoke(isHots);
    }

    public void SetCode(string code)
    {
        OnGetClientCode?.Invoke(code);
    }
}
