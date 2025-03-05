using MessagePack;

namespace Ulf
{

    [MessagePackObject]
    public struct UniversalAction : INextAction
    {
        [Key(0)]
        public ActionType action;
        [Key(1)]
        public int paramNum;

        public void DoAction(Unit unit)
        {
            unit.Actions.UniversalAction(action, paramNum);
        }
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
            unit.Move.MoveCommand(this);
        }
    }

    [Union(0, typeof(MovementAction))]
    [Union(1, typeof(StandAction))]
    [Union(2, typeof(UniversalAction))]
    public interface INextAction
    {
        void DoAction(Unit unit);
    }
}