using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;

namespace Ulf
{
    public class SceneGenerator 
    {
        private List<CreatePlanetStruct> planetList;
        private List<BridgePositionStruct> bridgeList;
        private AllUnitsScriptable allUnits;
        private int nextId;

        public SceneGenerator(int planetLimit, int idFrom, AllUnitsScriptable allUnits)
        {
            nextId = idFrom;
            this.allUnits = allUnits;
            planetList = new(planetLimit);
            for(int p = 0; p < planetLimit; p++)
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

        private CreatePlanetStruct GeneratePlanet(ElementType elementType)
        {
            int size = new Random().Next(0, 4);
            int unitCount = new Random().Next(1, 10);
            var availableUnits = allUnits.AllUnits.Where(u => u.ElementType == elementType);

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
                planetSize = size,
            };
        }
    }
}