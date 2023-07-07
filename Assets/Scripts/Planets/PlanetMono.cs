using System.Linq;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class PlanetMono : MonoBehaviour
    {
        [SerializeField] private UnitMono[] startUnits;
        [SerializeField] private ElementType elementType;

        Planet planet;

        CircleCollider2D planetCollider;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
            planet = new Planet(planetCollider.radius, elementType);
        }

        private void InstUnits(IRegister<Unit> unitsRegister)
        {
            float arcPerUnit = 360f / startUnits.Length;
            for (int u = 0; u < startUnits.Length; u++)
            {
                var _unitMono = Instantiate(startUnits[u], gameObject.transform);
                (float, float) freeArc = (u * arcPerUnit, u * (arcPerUnit +1));
                _unitMono.Init(planet, freeArc);
                unitsRegister.Record(_unitMono.Unit);
            }
        }

        public CreatePlanetStruct GeneratePlanetStruct()
        {
            return new()
            {
                 ElementType = elementType,
                 createUnits = startUnits.Select(unit => unit.UnitStruct).ToArray()
            };
        }

        private void OnValidate()
        {
            //SceneHub sceneHub = FindObjectOfType<SceneHub>();
            //sceneHub.UpdateScene(this);
        }
    }
}