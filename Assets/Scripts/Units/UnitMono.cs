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

        public void Init(Planet planet, (float, float) freeArc)
        {
            _unit = new Unit(planet.Element, unitStruct);
            movement.Init(planet, unitStruct, freeArc);
        }

        //protected void OnValidate()
        //{
        //    if (unitStruct.View.LastIndexOf(" ") == -1)
        //        return;
        //    var unitLoader = Resources.Load<AllUnitsScriptable>("Units");
        //    unitStruct.View.TrimEnd();
        //    unitLoader.AddUnit(unitStruct);
        //}
    }
}