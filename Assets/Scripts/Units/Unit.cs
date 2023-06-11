using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        private Planet planet;
        private IMovable movable;
        private IAttackable attackable;

        public IMovable Movable => movable;
        public IAttackable Attackable => attackable;
        public Planet Planet => planet;

        public Unit(Planet planet, IMovable movable, IAttackable attackable)
        {
            this.planet = planet;
            this.movable = movable;
            this.attackable = attackable;
        }
    }
}