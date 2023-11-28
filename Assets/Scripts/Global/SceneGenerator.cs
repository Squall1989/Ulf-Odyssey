using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;
using UnityEngine;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Ulf
{
    public class SceneGenerator 
    {

        private List<CreatePlanetStruct> planetList;
        private List<BridgePositionStruct> bridgeList;
        //private AllUnitsScriptable allUnits;
        private int planetNextId;
        private int unitNextId;

        private float bridgeSize = 5f;

        protected AllUnitsScriptable _allUnits;
        private AllPlanetsScriptable _allPlanets;

        public List<CreatePlanetStruct> PlanetList => planetList;

        public SceneGenerator(AllUnitsScriptable allUnits, AllPlanetsScriptable allPlanets)
        {
            _allUnits = allUnits;
            _allPlanets = allPlanets;
            Generate(10);
        }

        void Generate(int limit)
        {

            planetNextId = 0;
            planetList = new(limit);
            for (int p = 0; p < limit; p++)
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
                float bridgeAngle = Random.Range(15, 90);
                bool left = Random.Range(1, 2) % 2 == 0;
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
                int x = Random.Range(-60, 60);
                int y = Random.Range(-60, 60);
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

            int sizeNum = Random.Range(0, elementSizes.Length);
            Vector3 pos = RandomPlanetPos(elementSizes[sizeNum]);
            int unitCount = Random.Range(1, 10);
            var availableUnits = _allUnits.AllUnits.Where(u => u.ElementType == elementType);

            List<CreateUnitStruct> units = new(unitCount);
            
            for(int i = 0; i < unitCount; i++)
            {
                var defaultUnit = availableUnits.RandomElement();
                CreateUnitStruct createUnit = new CreateUnitStruct()
                {
                     View = defaultUnit.View,
                     Guid = unitNextId++,
                };
                units.Add(createUnit);
            }

            return new CreatePlanetStruct()
            {
                createUnits = units.ToArray(),
                ElementType = elementType,
                planetId = planetNextId++,
                planetSize = elementSizes[sizeNum],
                planetPos = pos,
            };
        }

        public SnapUnitStruct[] StartSnapUnits(CreateUnitStruct[] createUnits)
        {
            float arcPerUnit = 360f / createUnits.Length;
            SnapUnitStruct[] snapUnits = new SnapUnitStruct[createUnits.Length];
            for (int u = 0; u < createUnits.Length; u++)
            {
                (float, float) freeArc = (u * arcPerUnit, u * (arcPerUnit + 1));
                float startAngle = new System.Random().Next((int)freeArc.Item1, (int)freeArc.Item2);

                snapUnits[u] = new SnapUnitStruct()
                {
                    createUnit = createUnits[u],
                    angle = startAngle,
                    health = createUnits[u].Health,
                };

                //unitsRegister.Record(_unitMono.Unit);
            }

            return snapUnits;

        }
    }
}