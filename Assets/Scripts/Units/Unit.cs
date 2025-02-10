using System;
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        protected CircleMove _movement;
        protected CreateUnitStruct _unitStruct;
        protected DefaultUnitStruct _defaultUnit;
        protected Health _health;

        public int GUID => _unitStruct.Guid;
        public CircleMove Movement => _movement;

        public Unit(ElementType elementType, CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, CircleMove circleMove)
        {
            _defaultUnit = defaultUnit;
            _unitStruct = unitStruct;
            _health = new Health(defaultUnit.Health, elementType);
            _movement = circleMove;
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