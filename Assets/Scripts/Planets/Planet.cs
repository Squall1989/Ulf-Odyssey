using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Planet : IRound
    {
        private float radius;
        private List<Unit> units;

        public Planet(float radius)
        {
            this.radius = radius;
            units = new List<Unit>();
        }

        public virtual void NewUnit(Unit unit, float startDegree)
        {
            units.Add(unit);
            unit.Movable.ToLand(radius, startDegree);
        }

        public void RmUnit(Unit unit)
        {
            units.Remove(unit);
        }

        public IEnumerable<IMovable> GetAttackables(AttackableType attackableType, Vector2 pos, float interestDist)
        {

            return units.Where(unit => unit.Attackable.AttackableType == attackableType
                    && (unit.Movable.Position - pos).magnitude <= interestDist)
                    .Select(unit => unit.Movable);
        }
    }
}