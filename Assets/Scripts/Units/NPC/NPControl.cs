using System;

namespace Ulf
{
    public class NPControl : IControl
    {
        public Action<int> OnMove { get; set; }

    }
}