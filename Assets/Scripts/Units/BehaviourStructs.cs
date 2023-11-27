namespace Ulf
{
    public struct BehaviourUnitStruct
    {
        public BehaviourUnitStruct(Timer timer, INextAction nextAction)
        {
            this.timer = timer;
            this.nextAction = nextAction;
        }

        public INextAction nextAction;
        public Timer timer;
    }

    public struct MovementAction : INextAction
    {
        public int direction;

        public void DoAction(Unit unit)
        {
            unit.Movement.Move(direction);
        }
    }

    public interface INextAction
    {
        void DoAction(Unit unit);
    }
}