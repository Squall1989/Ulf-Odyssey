
using System.Collections.Generic;

namespace Ulf
{

    public class SceneHost
    {
        protected List<Unit> units = new List<Unit>();
        protected List<Planet> planets = new List<Planet>();

        public SceneHost(int planetCount, int unitCount) 
        {
            planets = new List<Planet>(planetCount);
            units = new List<Unit>(unitCount);
        }

        public void Add(Unit unit)
        {
            units.Add(unit);
        }

        public void Add(Planet planet)
        {
            planets.Add(planet);
        }
    }
}