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
}