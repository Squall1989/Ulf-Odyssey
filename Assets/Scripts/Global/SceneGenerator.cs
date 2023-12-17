using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using System.Drawing;
using UnityEngine.UIElements;

namespace Ulf
{
    public class SceneGenerator 
    {
        public List<(Vector2 pos, float size)> planetPoses = new();
        private List<CreatePlanetStruct> planetList = new List<CreatePlanetStruct>(limit);
        //private AllUnitsScriptable allUnits;
        private int planetNextId;
        private int unitNextId;

        private float bridgeSize = 5f;

        private const int limit = 5;
        private int bridgesGenerations;

        protected AllUnitsScriptable _allUnits;
        private AllPlanetsScriptable _allPlanets;

        public List<CreatePlanetStruct> PlanetList => planetList;

        public SceneGenerator(AllUnitsScriptable allUnits, AllPlanetsScriptable allPlanets, BridgeMono bridgeMono)
        {
            bridgeSize = bridgeMono.Size;
            _allUnits = allUnits;
            _allPlanets = allPlanets;
            bridgesGenerations = 0;
            GeneratePlanet(ElementType.wood, Vector2.zero, 1);
        }

        private (CreateBridgeStruct bridge, bool success) GetBridge(int planetId, Vector2 planetPos, float planetSize, float endPlanetSize)
        {
            float bridgeAngle = Random.Range(0, 359);
            Vector2 nextPos = CircleMove.GetMovePos(planetPos, planetSize + bridgeSize + endPlanetSize, bridgeAngle);
            int trying = 25;
            while (!checkPlanetsPos(nextPos, endPlanetSize))
            {
                bridgeAngle = Random.Range(0, 359);
                nextPos = CircleMove.GetMovePos(planetPos, planetSize + bridgeSize + endPlanetSize, bridgeAngle);

                if (--trying == 0)
                {
                    return (default, false);
                }
            }

            bool left = Random.Range(1, 2) % 2 == 0;
            var bridge = new CreateBridgeStruct()
            {
                angleStart = bridgeAngle,
                startPlanetId = planetId,
                mirrorLeft = left
            };

            return (bridge, true);
        }



        bool checkPlanetsPos(Vector2 rndPos, float size)
        {
            foreach (var planet in planetPoses)
            {
                if ((planet.pos - rndPos).magnitude < planet.size + size + bridgeSize)
                    return false;
            }

            return true;
        }

        private void GeneratePlanet(ElementType elementType, Vector2 pos, int sizeNum)
        {
            var planetId = planetNextId++;
            var elementSizes = _allPlanets.GetSizes(elementType);

            planetPoses.Add((pos, elementSizes[sizeNum]));

            int unitCount = Random.Range(1, 6);
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

            List<CreateBridgeStruct> createBridges = new();

            AddBridges(planetId, pos, sizeNum, elementType, elementSizes, ref createBridges);
            List<float> buildAngles = createBridges.Select(p => p.angleStart).ToList();
            AddBuilds(buildAngles);
            AddPlanet();

            void AddPlanet()
            {
                planetList.Add(new CreatePlanetStruct()
                {
                    createUnits = units.ToArray(),
                    ElementType = elementType,
                    planetId = planetId,
                    planetSize = elementSizes[sizeNum],
                    planetPos = pos,
                    bridges = createBridges.ToArray(),
                });
            }
        }

        private void AddBuilds(List<float> buildAngles)
        {

        }

        protected void AddBridges(int planetId, Vector2 pos, int sizeNum, ElementType elementType, float[] elementSizes, ref List<CreateBridgeStruct> createBridges)
        {
            if (bridgesGenerations++ >= limit)
                return;

            int bridgesOnPlanet = Random.Range(1, 4);

            while (bridgesOnPlanet-- > 0)
            {
                
                int nextSizeNum = Random.Range(0, elementSizes.Length);
                var bridgeCreation = GetBridge(planetId, pos, elementSizes[sizeNum], elementSizes[nextSizeNum]);
                if (bridgeCreation.success)
                {
                    createBridges.Add(bridgeCreation.bridge);

                    var distToNextPlanet = elementSizes[sizeNum] + bridgeSize + elementSizes[nextSizeNum];
                    var nextPlanet = CircleMove.GetMovePos(pos, distToNextPlanet, bridgeCreation.bridge.angleStart);
                    GeneratePlanet(elementType, nextPlanet, nextSizeNum);
                }
                else
                {
                    break;
                }
            }
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