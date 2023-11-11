using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Ulf
{
    public class SceneGenerator 
    {

        private List<CreatePlanetStruct> planetList;
        private List<BridgePositionStruct> bridgeList;
        //private AllUnitsScriptable allUnits;
        private int nextId;

        private float bridgeSize = 5f;

        protected IGame _game;
        protected AllUnitsScriptable _allUnits;
        private AllPlanetsScriptable _allPlanets;

        public List<CreatePlanetStruct> PlanetList => planetList;

        public SceneGenerator(IGame game, AllUnitsScriptable allUnits, AllPlanetsScriptable allPlanets)
        {
            _game = game;
            _allUnits = allUnits;
            _allPlanets = allPlanets;
            Generate();
        }

        protected async void Generate()
        {
            var result = await _game.GetPlanetsLimit();

            nextId = result.from;
            planetList = new(result.limit);
            for (int p = 0; p < result.limit; p++)
            {
                planetList.Add(GeneratePlanet(ElementType.wood));
            }
            GenerateScenePositions();
        }

        private void GenerateScenePositions()
        {
            bridgeList = new();
            foreach(var planet in planetList)
            {
                float bridgeAngle = new Random().Next(15, 90);
                bool left = new Random().Next(1, 2) % 2 == 0;
                bridgeList.Add(new BridgePositionStruct()
                {
                    angleStart = bridgeAngle,
                    startPlanetId = planet.planetId,
                    mirrorLeft = left
                });
            }
        }

        private Vector3 RandomPlanetPos(float size)
        {
            Vector3 rndPos = new Vector3();
            while (!checkPlanetsPos())
            {
                int x = UnityEngine.Random.Range(-60, 60);
                int y = UnityEngine.Random.Range(-60, 60);
                rndPos = new Vector3(x, y, 0);
            }

            return rndPos;

            bool checkPlanetsPos()
            {
                foreach(var planet in planetList)
                {
                    if((planet.planetPos - rndPos).magnitude < planet.planetSize + size + bridgeSize) 
                        return false;
                }

                return true;
            }
        }

        private CreatePlanetStruct GeneratePlanet(ElementType elementType)
        {
            var elementSizes = _allPlanets.GetSizes(elementType);

            int sizeNum = new Random().Next(0, elementSizes.Length);
            Vector3 pos = RandomPlanetPos(elementSizes[sizeNum]);
            int unitCount = new Random().Next(1, 10);
            var availableUnits = _allUnits.AllUnits.Where(u => u.ElementType == elementType);

            List<CreateUnitStruct> units = new(unitCount);
            
            for(int i = 0; i < unitCount; i++)
            {
                units.Add(availableUnits.RandomElement());
            }

            return new CreatePlanetStruct()
            {
                createUnits = units.ToArray(),
                ElementType = elementType,
                planetId = nextId++,
                planetSize = elementSizes[sizeNum],
                planetPos = pos,
            };
        }
    }
}