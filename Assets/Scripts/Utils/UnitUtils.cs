using UnityEngine;

namespace Ulf
{
    public static class UnitUtils
    {
        public static bool IsUnitLookTo(Unit unit, Vector2 point)
        {
            Vector2 pointPlanetVector = point - unit.Move.PlanetPosition;
            Vector2 unitPlanetVector = unit.Move.Position - unit.Move.PlanetPosition;
            bool pointRight = MathUtils.IsRightDir(unitPlanetVector, pointPlanetVector);

            bool lookToPoint = pointRight == (unit.Move.Direct == -1);
            return lookToPoint;
        }
    }
}