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

        public void InstUnits(UnitMono[] unitsMono)
        {
            float arcPerUnit = 360f / unitsMono.Length;
            for (int u = 0; u < unitsMono.Length; u++)
            {
                var _unitMono = Instantiate(unitsMono[u], gameObject.transform);
                (float, float) freeArc = (u * arcPerUnit, u * (arcPerUnit +1));
                _unitMono.Init(planet, freeArc);
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