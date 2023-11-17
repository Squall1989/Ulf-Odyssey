
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    /// <summary>
    /// Units interaction
    /// </summary>
    public class SceneHost : ISceneProxy
    {

        int planetCount = 10;
        protected List<Unit> units = new List<Unit>();
        protected List<Planet> planets;

        private SceneGenerator _sceneGenerator;

        public SceneHost(SceneGenerator sceneGenerator) 
        {
            _sceneGenerator = sceneGenerator;
            planets = new List<Planet>(planetCount);
        }

        public void Add(Unit unit)
        {
            units.Add(unit);
        }

        public void Add(Planet planet)
        {
            planets.Add(planet);
        }

        public Task<SnapSceneStruct> GetSceneStruct()
        {
            var planetsSnapshot = new SnapPlanetStruct[planets.Count];
            for (int i = 0; i < planets.Count; i++)
            {
                planetsSnapshot[i] = planets[i].GetSnapshot();
            }

            return Task.FromResult(new SnapSceneStruct()
            {
                snapPlanets = planetsSnapshot,
            });
        }

    }
}