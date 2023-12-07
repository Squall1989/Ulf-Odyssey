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
            if (planetStruct.bridges != null)
            {
                foreach (var bridge in planetStruct.bridges)
                {
                    var bridgeMono = Instantiate(bridgePfb);
                    bridgeMono.Init(planet, bridge.angleStart);
                }
            }
        }

        public void InstUnits(UnitMono[] unitsMono, SnapUnitStruct[] snapUnits, IUnitsProxy unitsProxy)
        {
            for (int u = 0; u < unitsMono.Length; u++)
            {
                var _unitMono = Instantiate(unitsMono[u], gameObject.transform);
                _unitMono.Init(planet, snapUnits[u].createUnit, snapUnits[u].angle);
                unitsProxy.Add(_unitMono.Unit);
                //unitsRegister.Record(_unitMono.Unit);
            }
        }

        

        private void OnValidate()
        {
            //SceneHub sceneHub = FindObjectOfType<SceneHub>();
            //sceneHub.UpdateScene(this);
        }
    }
}