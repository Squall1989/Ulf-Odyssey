using System;
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        protected CircleMove _movement;
        private ActionUnit _action;
        protected CreateUnitStruct _unitStruct;
        protected DefaultUnitStruct _defaultUnit;
        protected Health _health;

        public string View => _unitStruct.View;
        public int GUID => _unitStruct.Guid;
        public float Degree => _movement.Degree;
        public CircleMove Move => _movement;
        public ActionUnit Actions => _action;
        public Health Health => _health;

        public Unit(CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, 
            CircleMove circleMove, ActionUnit action, Health health)
        {
            _defaultUnit = defaultUnit;
            _unitStruct = unitStruct;
            _health = health;
            _movement = circleMove;
            _action = action;
        }

        internal SnapUnitStruct GetSnapshot()
        {
            return new SnapUnitStruct()
            {
                angle = _movement.Degree,
                health = _health.CurrHealth,
                createUnit = _unitStruct,
            };
        }
    }
}