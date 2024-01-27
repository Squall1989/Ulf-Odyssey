using System.Linq;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class PlanetMono : MonoBehaviour
    {
        //[SerializeField] private UnitMono[] startUnits;
        [SerializeField] private ElementType elementType;
        [SerializeField] private BridgeMono bridgePfb;

        private Planet planet;

        private CircleCollider2D planetCollider;

        public ElementType ElementType => elementType;
        public float Size => GetComponent<CircleCollider2D>().radius;

        public Planet Planet => planet;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
        }

        public void Init(CreatePlanetStruct planetStruct)
        {
            planet = new(planetStruct);
            gameObject.name = planetStruct.planetId.ToString();
            if (planetStruct.bridges != null)
            {
                foreach (var bridge in planetStruct.bridges)
                {
                    var bridgeMono = Instantiate(bridgePfb);
                    bridgeMono.Init(planet, bridge.angleStart);
                }
            }
        }

        public void InstBuilds(BuildMono[] builds, CreateBuildStruct[] createBuilds)
        {
            for (int u = 0; u < builds.Length; u++)
            {
                var _buildMono = Instantiate(builds[u]);
                _buildMono.Init(planet, createBuilds[u]);
            }
        }

        public void InstUnits(UnitMono[] unitsMono, SnapUnitStruct[] snapUnits, IUnitsProxy unitsProxy)
        {
            for (int u = 0; u < unitsMono.Length; u++)
            {
                InstUnit(unitsMono[u], snapUnits[u], unitsProxy);
            }
        }

        public UnitMono InstUnit(UnitMono unitMono, SnapUnitStruct snapUnit, IUnitsProxy unitsProxy)
        {
            var _unitMono = Instantiate(unitMono, gameObject.transform);
            _unitMono.Init(planet, snapUnit.createUnit, snapUnit.angle);
            if(unitsProxy != null)
                unitsProxy.Add(_unitMono.Unit);
            return _unitMono;
        }
            

        private void OnValidate()
        {
            //SceneHub sceneHub = FindObjectOfType<SceneHub>();
            //sceneHub.UpdateScene(this);
        }
    }
}