using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{

    public class UnitMono : MonoBehaviour
    {
        [SerializeField] protected MovementMono _movement;
        [SerializeField] protected ActionsMono _action;

        [SerializeField] protected DefaultUnitStruct defaultUnit;

        protected Unit _unit;
        
        public virtual Unit Unit => _unit;
        public DefaultUnitStruct DefaultUnit => defaultUnit;
        public MovementMono MovementMono => _movement;
        public CircleMove CircleMove => _movement.CircleMove;

        public virtual void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            var action = new ActionUnit(createUnit.Guid);
            var health = new Health(defaultUnit.Health, defaultUnit.ElementType);

            _action.Init(action);
            _movement.Init(planet, freeArc);

            _unit = new Unit(createUnit, defaultUnit, _movement.CircleMove, action, health);

            health.SetKillables(GetKillables());
        }

        protected virtual List<IKillable> GetKillables()
        {
            List<IKillable> killables = new()
            {
                CircleMove, 
                _action.Action,
                _action,
                _movement,
            };
            return killables;
        }
    }
}