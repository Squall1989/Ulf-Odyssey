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
        public int direction;

        public void DoAction(Unit unit)
        {
            (unit as Player).ExtendedCircleMove.SetStandDirect(direction);
        }
    }

    [MessagePackObject]
    public struct MovementAction : INextAction
    {
        [Key(0)]
        public int direction;
        [Key(1)]
        public float fromAngle;

        public void DoAction(Unit unit)
        {
            unit.Movement.SetMoveDirect(direction);
            // ToDo: smooth angle set
            unit.Movement.SetAngle(fromAngle);
        }
    }

    [Union(0, typeof(MovementAction))]
    public interface INextAction
    {
        void DoAction(Unit unit);
    }
}