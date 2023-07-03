using Assets.Scripts.Interfaces;
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
        }
        // Start is called before the first frame update
        void Start()
        {
            planet = new Planet(planetCollider.radius, elementType);
        }

        [Inject]
        private void InstUnits(IRegister<Unit> unitsRegister)
        {
            foreach (UnitMono unitPref in startUnits)
            {
               var _unit = Instantiate(unitPref, gameObject.transform);
                _unit.Init(planet);
                unitsRegister.Record(_unit.Unit);
            }
        }


        private void OnValidate()
        {
            SceneHub sceneHub = FindObjectOfType<SceneHub>();
            sceneHub.UpdateScene(this);
        }
    }
}