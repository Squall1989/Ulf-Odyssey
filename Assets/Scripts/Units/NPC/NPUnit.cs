
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class NPUnit : Unit
    {
        public NPUnit(Planet planet, CreateUnitStruct unitStruct, IMovable circleMove) : base(planet, unitStruct, circleMove)
        {
        }
    }
}