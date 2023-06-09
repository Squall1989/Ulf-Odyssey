using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Bridge : Planet
    {
        private float startDeg, endDeg;

        public Bridge(float radius, float startDeg, float endDeg) : base(radius)
        {
            this.startDeg = startDeg;
            this.endDeg = endDeg;
        }

        public override void NewMovable(IMovable movable, float startDegree)
        {
            base.NewMovable(movable, startDegree);
        }
    }
}