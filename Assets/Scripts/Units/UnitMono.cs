using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        [SerializeField] protected MovementMono movement;
        [SerializeField] protected DefaultUnitStruct defaultUnit;

        protected Unit _unit;
        
        public virtual Unit Unit => _unit;
        public DefaultUnitStruct DefaultUnit => defaultUnit;
        public CircleMove CircleMove => movement.CircleMove;

        public virtual void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            movement.Init(planet, new CircleMove(defaultUnit.MoveSpeed), freeArc);
            _unit = new Unit(planet.Element, createUnit, defaultUnit, movement.CircleMove);
        }
    }
}