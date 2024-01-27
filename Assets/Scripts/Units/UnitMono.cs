using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        [SerializeField] private MovementMono movement;
        [SerializeField] private DefaultUnitStruct defaultUnit;

        protected Unit _unit;
        
        public Unit Unit => _unit;
        public DefaultUnitStruct DefaultUnit => defaultUnit;
        public CircleMove CircleMove => movement.CircleMove;

        public void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            movement.Init(planet, defaultUnit, freeArc);
            _unit = new Unit(planet.Element, createUnit, defaultUnit, movement.CircleMove);
            planet.AddUnit(_unit);
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