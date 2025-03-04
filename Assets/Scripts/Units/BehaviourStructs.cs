using MessagePack;

namespace Ulf
{
    public struct BehaviourUnitStruct
    {
        public BehaviourUnitStruct(Timer timer, INextAction nextAction)
        {
            this.timer = timer;
            this.prevAction = nextAction;
        }

        public INextAction prevAction;
        public Timer timer;
    }

    [MessagePackObject]
    public struct StandAction : INextAction
    {
        [Key(0)]
        public float degree;
        [Key(1)]
        public int roundId;

        public void DoAction(Unit unit)
        {
            (unit as Player).StandTo(roundId, degree);
        }
    }

    [MessagePackObject]
    public struct MovementAction : INextAction
    {
        [Key(0)]
        public int direction;
        [Key(1)]
        public float fromAngle;
        [Key(2)]
        public float speed;

        public void DoAction(Unit unit)
        {
            unit.MoveCommand(this);
        }
    }

    [Union(0, typeof(MovementAction))]
    [Union(1, typeof(StandAction))]
    public interface INextAction
    {
        void DoAction(Unit unit);
    }
}