using System;
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        private CircleMove _movement;
        private CreateUnitStruct _unitStruct;
        private Health _health;

        public CircleMove Movement => _movement;

        public Unit(ElementType elementType, CreateUnitStruct unitStruct)
        {
            _unitStruct = unitStruct;
            _health = new Health(unitStruct.Health, elementType);
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