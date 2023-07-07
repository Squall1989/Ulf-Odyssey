using System;
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        private CircleMove _movement;
        private Health _health;

        public CircleMove Movement => _movement;

        public Unit(ElementType elementType, CreateUnitStruct unitStruct)
        {

            _health = new Health(unitStruct.Health, elementType);
        }
    }
}