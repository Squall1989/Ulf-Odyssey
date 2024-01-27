
using Extensions;
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zenject;

namespace Ulf
{
    /// <summary>
    /// Units interaction
    /// </summary>
    public class SceneHost : ISceneProxy
    {

        protected List<Planet> planets = new();
        private SceneGenerator _sceneGenerator;

        public Action<SnapPlayerStruct> OnPlayerCreated;

        public SceneHost(SceneGenerator sceneGenerator) 
        {
            _sceneGenerator = sceneGenerator;
        }

        public void AddPlanet(Planet planet)
        {
            planets.Add(planet);
        }

        public void AddPlayer(Unit unit)
        {

        }

        public Task<List<SnapPlanetStruct>> GetSceneStruct()
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

                return Task.FromResult(
                
                    planetsSnapshot.ToList()
                );
            }
            else
            {
                SnapPlanetStruct[] planetsSnapshot = new SnapPlanetStruct[planets.Count];
                for (int i = 0; i < planets.Count; i++)
                {
                    planetsSnapshot[i] = planets[i].GetSnapshot();
                }
                
                return Task.FromResult(
                    planetsSnapshot.ToList()
                );
            }
        }

        public async Task<SnapPlayerStruct> SpawnPlayer()
        {
            while(planets.Count == 0)
            {
                await Task.Delay(1000);
            }

            var playerStruct = _sceneGenerator.SpawnPlayer();

            OnPlayerCreated?.Invoke(playerStruct);
            return playerStruct;
        }

    }
}