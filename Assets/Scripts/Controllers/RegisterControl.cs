
using System.Collections.Generic;

namespace Ulf
{
    public class RegisterControl : IRegister<IControl>
    {
        int count;
        private Dictionary<int, IControl> controlDict = new();
        
        public IControl GetComponent(int guid)
        {
            return controlDict[guid];
        }

        public void Record(IControl component)
        {
            controlDict.Add(count++, component);
        }
    }
}
