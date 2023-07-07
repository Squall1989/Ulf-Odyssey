using System.Collections.Generic;

namespace Ulf
{
    public class RegisterPlanet : IRegister<Planet>
    {
        int countPlanet = 0;
        private Dictionary<int, Planet> planetDict = new(); // planets, bridges

        public Planet GetComponent(int guid)
        {
            return planetDict[guid];
        }

        public void Record(Planet component)
        {
            planetDict.Add(countPlanet++, component);
        }
    }
}
