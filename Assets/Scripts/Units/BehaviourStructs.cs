using MessagePack;

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
    [MessagePackObject]
    public struct MovementAction : INextAction
    {
        [Key(0)]
        public int direction;

        public void DoAction(Unit unit)
        {
            unit.Movement.SetMoveDirect(direction);
        }
    }

    [Union(0, typeof(MovementAction))]
    public interface INextAction
    {
        void DoAction(Unit unit);
    }
}