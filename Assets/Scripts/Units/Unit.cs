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

        public Action<float> OnChangeSpeed;

        public string View => _unitStruct.View;
        public int GUID => _unitStruct.Guid;
        public float Degree => _movement.Degree;

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

        public virtual void MoveCommand(MovementAction action)
        {
            _movement.SetAngle(action.fromAngle);
            _movement.SetMoveDirect(action.direction);
            _movement.SetSpeed(action.speed);
            OnChangeSpeed?.Invoke(action.speed);
        }
    }
}