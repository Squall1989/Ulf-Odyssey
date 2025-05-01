using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class PlanetMono : MonoBehaviour, IRoundMono
    {
        //[SerializeField] private UnitMono[] startUnits;
        [SerializeField] private ElementType elementType;
        [SerializeField] private BridgeMono bridgePfb;

        private Planet planet;

        private CircleCollider2D planetCollider;

        private BridgeMono[] bridgesMono;

        public BridgeMono[] BridgesMono => bridgesMono;
        public ElementType ElementType => elementType;
        public float Size => GetComponent<CircleCollider2D>().radius;

        public Planet Planet => planet;

        public Transform TransformRound => transform;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
        }

        public void Init(CreatePlanetStruct planetStruct)
        {
            planet = new(planetStruct, this);
            gameObject.name = planetStruct.planetId.ToString();
            if (planetStruct.bridges != null)
            {
                bridgesMono = new BridgeMono[planetStruct.bridges.Length];
                for (int i = 0; i < planetStruct.bridges.Length; i++)
                {
                    var bridgeMono = Instantiate(bridgePfb);
                    bridgeMono.Init(planet, planetStruct.bridges[i]);

                    bridgesMono[i] = bridgeMono;
                }
            }
        }

        public void InstBuilds(BuildMono[] builds, List<UnitMono> unitMonoList, CreateBuildStruct[] createBuilds)
        {
            for (int u = 0; u < builds.Length; u++)
            {
                var _buildMono = Instantiate(builds[u]);
                _buildMono.Init(planet, createBuilds[u]);

                if (_buildMono is TowerBuildMono tower)
                {
                    var guids = createBuilds[u].UnitGuids;

                    for (int g = 0; g < guids.Length; g++)
                    {
                        var unitInTower = unitMonoList.First(p => p.Unit.GUID == guids[g]);
                        (unitInTower.MovementMono as TowerMovementMono).StandTower(tower.UnitPlatform);
                    }
                }
            }
        }

        public List<UnitMono> InstUnits(UnitMono[] unitsMono, SnapUnitStruct[] snapUnits, IUnitsProxy unitsProxy)
        {
            var unitList = new List<UnitMono>(unitsMono.Length);
            for (int u = 0; u < unitsMono.Length; u++)
            {
                unitList.Add(InstUnit(unitsMono[u], snapUnits[u], unitsProxy));
            }
            return unitList;
        }

        public UnitMono InstUnit(UnitMono unitMono, SnapUnitStruct snapUnit, IUnitsProxy unitsProxy)
        {
            var _unitMono = Instantiate(unitMono, gameObject.transform);
            _unitMono.Init(planet, snapUnit.createUnit, snapUnit.angle);
            if (unitsProxy != null)
            {
                unitsProxy.Add(_unitMono.Unit);
                planet.AddUnit(_unitMono.Unit);

            }
            return _unitMono;
        }

        public float LookAtCenter(Transform unitTransform)
        {
            Vector3 relative = -unitTransform.InverseTransformPoint(transform.position);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            
            return -angle;
        }
    }
}