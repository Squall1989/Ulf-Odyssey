using System;

public interface IControl
{
    Action<int> OnMove { get; set; }
}
