using System.Linq;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class PlanetMono : MonoBehaviour
    {
        //[SerializeField] private UnitMono[] startUnits;
        [SerializeField] private ElementType elementType;

        private Planet planet;

        private CircleCollider2D planetCollider;

        public ElementType ElementType => elementType;
        public float Size => GetComponent<CircleCollider2D>().radius;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
        }

        public void Init(CreatePlanetStruct planetStruct)
        {
            planet = new(planetStruct);
        }

        public void InstUnits(UnitMono[] unitsMono, SnapUnitStruct[] snapUnits)
        {
            for (int u = 0; u < unitsMono.Length; u++)
            {
                var _unitMono = Instantiate(unitsMono[u], gameObject.transform);
                _unitMono.Init(planet, snapUnits[u].angle);
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