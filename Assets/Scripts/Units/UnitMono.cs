using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        [SerializeField] protected MovementMono _movement;
        [SerializeField] protected AnimatorMono _animator;

        [SerializeField] protected DefaultUnitStruct defaultUnit;

        protected Unit _unit;
        
        public virtual Unit Unit => _unit;
        public DefaultUnitStruct DefaultUnit => defaultUnit;
        public CircleMove CircleMove => _movement.CircleMove;

        public virtual void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            _movement.Init(planet, new CircleMove(), freeArc);
            _unit = new Unit(planet.Element, createUnit, defaultUnit, _movement.CircleMove);
            _unit.OnChangeSpeed += _animator.SetSpeed;
        }
    }
}