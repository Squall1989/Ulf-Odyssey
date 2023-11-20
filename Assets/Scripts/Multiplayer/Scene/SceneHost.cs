
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

        protected List<Unit> units = new List<Unit>();
        protected List<Planet> planets;
        private SceneGenerator _sceneGenerator;

        public SceneHost(SceneGenerator sceneGenerator) 
        {
            _sceneGenerator = sceneGenerator;
            planets = new List<Planet>();
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
            if (planets.Count == 0)
            {
                var planetsCreate = _sceneGenerator.PlanetList;
                SnapPlanetStruct[] planetsSnapshot = new SnapPlanetStruct[planetsCreate.Count];
                for (int i = 0; i < planetsCreate.Count; i++)
                {
                    planetsSnapshot[i] = new SnapPlanetStruct()
                    {
                        createPlanet = planetsCreate[i],
                        snapUnits = _sceneGenerator.StartSnapUnits(planetsCreate[i].createUnits),
                    };
                }

                return Task.FromResult(new SnapSceneStruct()
                {
                    snapPlanets = planetsSnapshot,
                });
            }
            else
            {
                SnapPlanetStruct[] planetsSnapshot = new SnapPlanetStruct[planets.Count];
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
}