using System.Collections.Generic;

namespace Ulf
{
    public class RegisterPlanet : IRegister<IRound>
    {
        int count = 0;
        private Dictionary<int, IRound> roundDict = new(); // planets, bridges

        public IRound GetComponent(int guid)
        {
            return roundDict[guid];
        }

        public void Record(IRound component)
        {
            roundDict.Add(count++, component);
        }
    }
}
