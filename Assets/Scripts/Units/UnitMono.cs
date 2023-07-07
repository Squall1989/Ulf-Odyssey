using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        [SerializeField] private MovementMono movement;
        [SerializeField] private CreateUnitStruct unitStruct;

        protected Unit _unit;
        
        public Unit Unit => _unit;
        public CreateUnitStruct UnitStruct => unitStruct;

        // Start is called before the first frame update
        void Start()
        {

        }


        public void Init(Planet planet, (float, float) freeArc)
        {
            _unit = new Unit(planet.Element, unitStruct);
            movement.Init(planet, unitStruct, freeArc);
        }
    }
}