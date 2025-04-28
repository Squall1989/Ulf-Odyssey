using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using static UnityEditor.Rendering.FilterWindow;

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

        private const int limit = 10;
        private int bridgesGenerations;

        protected AllUnitsScriptable _allUnits;
        private AllPlanetsScriptable _allPlanets;
        private AllBuildScriptable _allBuilds;
        private PlayerMono _playerMono;

        public List<CreatePlanetStruct> PlanetList => planetList;

        public SceneGenerator(AllUnitsScriptable allUnits, AllPlanetsScriptable allPlanets, AllBuildScriptable allBuilds, 
            BridgeMono bridgeMono, PlayerMono playerMono)
        {
            bridgeSize = bridgeMono.Size;
            _allUnits = allUnits;
            _allPlanets = allPlanets;
            _allBuilds = allBuilds;
            _playerMono = playerMono;
            bridgesGenerations = 0;

            planetList = CreateTriangleStruct(ElementType.wood).ToList();
        }

        private CreatePlanetStruct[] CreateTriangleStruct(ElementType element, int corners = 3)
        {
            var allSizes = _allPlanets.GetSizes(element);
            float[] planetSizes = new float[corners];
            Vector2[] planetPoses = new Vector2[corners];
            float[] edges = new float[corners];

            for (int i = 0; i < corners; i++)
            {
                planetSizes[i] = allSizes[Random.Range(0, allSizes.Length)];
            }

            planetPoses[0] = new Vector2(0, 0);

            for (int e = 0; e < corners; e++)
            {
                int nextCornerNum = e < corners -1 ? e +1 : 0;
                edges[e] = planetSizes[e] + planetSizes[nextCornerNum] + bridgeSize;
            }

            planetPoses[1] = new Vector2(edges[0], 0);

            Vector2 pos3 = new Vector2(edges[2], 0);
            var numerator = edges[0] * edges[0] + edges[2] * edges[2] - edges[1] * edges[1];
            var denominator = edges[0] * edges[2] * 2f;
            var cos3 = numerator / denominator;
            var sin3 = Math.Sqrt(1 - cos3 * cos3);
            float findedX = (float)(pos3.x * cos3 - pos3.y * sin3);
            float findedY = (float)(pos3.x * sin3 + pos3.y * cos3);

            planetPoses[2] = new Vector2(findedX, findedY);

            return CreatePlanetsFromPoses(ref planetPoses, ref planetSizes, element);
        }

        private CreatePlanetStruct[] CreatePlanetsFromPoses(ref Vector2[] planetPoses, ref float[] planetSizes, ElementType element)
        {
            int lenght = planetPoses.Length;
            var planetStructs = new CreatePlanetStruct[lenght];

            for (int p = 0; p < lenght; p++)
            {
                float _planetSize = planetSizes[p];
                int nextNum = p < lenght -1 ? p +1 : 0;
                var bridgeAngle = calcAngle(planetPoses[p], planetPoses[nextNum]);

                CreateBridgeStruct bridgeStruct = new CreateBridgeStruct()
                {
                      angleStart = bridgeAngle,
                      startPlanetId = p,
                      endPlanetId = nextNum,
                      mirrorLeft = p%2 ==0,
                };

                List<CreateUnitStruct> freeUnits = GenerateUnits(element);

                List<CreateBuildStruct> builds = GenerateBuilds(element, 1, _planetSize, bridgeAngle, out var createdUnits);

                freeUnits.AddRange(createdUnits);

                planetStructs[p] = new CreatePlanetStruct()
                {
                    ElementType = element,
                    planetId = planetNextId++,
                    planetPos = planetPoses[p],
                    planetSize = _planetSize,
                    createUnits = freeUnits.ToArray(),
                    builds = builds.ToArray(),
                    bridges = new CreateBridgeStruct[] { bridgeStruct }
                };
            }

            return planetStructs;

            float calcAngle(Vector2 start, Vector2 end)
            {
                var angledVect = end - start;
                float angle = Vector2.SignedAngle(new Vector2(1,0), angledVect);
                if (angle < 0)
                    angle += 360f;
                return angle;
            }
        }

        private (CreateBridgeStruct bridge, bool success) GenerateBridge(int planetId, Vector2 planetPos, float planetSize, float endPlanetSize)
        {
            float bridgeAngle = Random.Range(0, 359);
            Vector2 nextPos = CircleMove.GetMovePos(planetPos, planetSize + bridgeSize + endPlanetSize, bridgeAngle);
            int trying = 25;
            while (!checkPlanetsPos(nextPos, planetPos, endPlanetSize))
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

        bool checkPlanetsPos(Vector2 rndPos, Vector2 planetPos, float size)
        {
            foreach (var planet in planetPoses)
            {
                if (planetPos.Equals(rndPos))
                    continue;

                if ((planet.pos - rndPos).magnitude < planet.size + size + bridgeSize)
                    return false;
            }

            return true;
        }

        public SnapPlayerStruct SpawnPlayer()
        {
            var defaultPlayer = _playerMono.DefaultUnit;
            var planetId = planetList.RandomElement().planetId;

            var unitPlayer = GenerateUnit(defaultPlayer);
            return new SnapPlayerStruct()
            {
                planetId = planetId,
                snapUnitStruct = new SnapUnitStruct()
                {
                    createUnit = unitPlayer,
                    angle = Random.Range(0, 359f),
                    health = unitPlayer.Health
                }
            };
        }

        private CreateUnitStruct GenerateUnit(DefaultUnitStruct defaultUnit)
        {
            CreateUnitStruct createUnit = new CreateUnitStruct()
            {
                View = defaultUnit.View,
                Guid = unitNextId++,
                Health = defaultUnit.Health,
            };

            return createUnit;
        }

        private List<CreateUnitStruct> GenerateUnits(ElementType elementType)
        {
            int unitCount = Random.Range(1, 6);
            var availableUnits = _allUnits.AllUnits.Where(u => u.ElementType == elementType);

            var units = new List<CreateUnitStruct>(unitCount);

            for (int i = 0; i < unitCount; i++)
            {
                var defaultUnit = availableUnits.RandomElement();
                var createUnit = GenerateUnit(defaultUnit);
                units.Add(createUnit);
            }

            return units;
        }

        const float freeSpaceBetween = 2f;

        private bool FindFreeAngleOnPlanet(List<(float deg, float width)> reservSpaces, float radius,
            float nextWidth, out float angle)
        {
            for (int deg = 15; deg < 360; deg += 5)
            {
                bool isDegFree = true;

                for(int r = 0; r < reservSpaces.Count; r++)
                {
                    float degDiff = Math.Abs(reservSpaces[r].deg - nextWidth);
                    float arcLen = radius * (float)Math.PI * degDiff / 180f;

                    float spaceNeed = (reservSpaces[r].width + nextWidth) * .5f + freeSpaceBetween;

                    if(arcLen < spaceNeed)
                    {
                        isDegFree = false;
                        break;
                    }
                }

                if(isDegFree)
                {
                    angle = deg;
                    return true;
                }
            }

            angle = -1;
            return false;
        }

        private List<CreateBuildStruct> GenerateBuilds(ElementType element, int buildsLimit, 
            float planetSize, float bridgeAngle, out List<CreateUnitStruct> createdUnits)
        {
            createdUnits = new();
            List<CreateBuildStruct> createBuilds = new();

            List<(float angle, float width)> reservSpaces = new(buildsLimit +1) { (bridgeAngle, 4f) };

            for(int b = 0; b < buildsLimit; b++)
            {
                var buildMono = _allBuilds.Builds.Where(p => p.Element == element).RandomElement();
                float width = buildMono.size.x;

                if (!FindFreeAngleOnPlanet(reservSpaces,planetSize,width, out float angle))
                {
                    break;
                }

                DefaultBuildStruct buildStruct = buildMono.DefaultBuild;
                var spawnUnits = buildStruct.UnitsInside;

                for (int u = 0; u < spawnUnits.Length; u++)
                {
                    DefaultUnitStruct unitStruct = _allUnits.AllUnits.First(p => p.View == spawnUnits[u]);
                    CreateUnitStruct unit = GenerateUnit(unitStruct);

                    createdUnits.Add(unit);
                }

                createBuilds.Add(new CreateBuildStruct()
                { 
                    Angle = angle, 
                    View = buildStruct.View,
                    UnitGuids = createdUnits.Select(p => p.Guid).ToArray()
                });
            }

            return createBuilds;
        }

        protected void CreateBridgeWithPlanet(ref CreatePlanetStruct planetStruct, int sizeNum, int nextSizeNum)
        {
            if (bridgesGenerations++ >= limit)
                return;

            var elementSizes = _allPlanets.GetSizes(planetStruct.ElementType);
            var bridgeCreation = GenerateBridge(planetStruct.planetId, planetStruct.planetPos, elementSizes[sizeNum], elementSizes[nextSizeNum]);
            if (bridgeCreation.success)
            {
                int nextPlanetId = planetNextId++;
                bridgeCreation.bridge.endPlanetId = nextPlanetId;

                var distToNextPlanet = elementSizes[sizeNum] + bridgeSize + elementSizes[nextSizeNum];
                var nextPlanetPos = CircleMove.GetMovePos(planetStruct.planetPos, distToNextPlanet, bridgeCreation.bridge.angleStart);
                //GeneratePlanet(nextPlanetId, planetStruct.ElementType, nextPlanetPos, nextSizeNum);
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