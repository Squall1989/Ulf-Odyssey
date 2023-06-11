using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Unit
    {
        private IMovable movable;
        private IAttackable attackable;

        public IMovable Movable => movable;
        public IAttackable Attackable => attackable;

        public Unit(IMovable movable, IAttackable attackable)
        {
            this.movable = movable;
            this.attackable = attackable;
        }
    }
}