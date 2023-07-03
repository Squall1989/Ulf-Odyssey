using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        private Planet planet;
        private IMovable movable;
        private Health _health;

        public IMovable Movable => movable;
        public Planet Planet => planet;

        public Unit(Planet planet, CreateUnitStruct unitStruct, IMovable circleMove)
        {
            this.planet = planet;
            this.movable = circleMove;
            _health = new Health(unitStruct.Health, unitStruct.Element);
        }
    }
}