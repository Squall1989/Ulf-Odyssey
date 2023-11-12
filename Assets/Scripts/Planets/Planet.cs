using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Planet
    {
        private float _radius;
        private ElementType _element;
        private List<Unit> units;
        private Vector2 _position;

        public ElementType Element => _element;
        public float Radius => _radius;
        public Vector2 Position => _position;

        public Planet(float radius, ElementType element, Vector2 pos)
        {
            _radius = radius;
            _element = element;
            units = new List<Unit>();
            _position = pos;
        }

        public void RmUnit(Unit unit)
        {
            units.Remove(unit);
        }

        /*
        public IEnumerable<IMovable> GetAttackables(AttackableType attackableType, Vector2 pos, float interestDist)
        {

            return units.Where(unit => unit.Attackable.AttackableType == attackableType
                    && (unit.Movable.Position - pos).magnitude <= interestDist)
                    .Select(unit => unit.Movable);
        }
        */
    }
}