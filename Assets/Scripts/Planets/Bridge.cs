using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class Bridge : Planet
    {
        private float startDeg, endDeg;

        public Bridge(float radius, float startDeg, float endDeg) : base(radius, default)
        {
            this.startDeg = startDeg;
            this.endDeg = endDeg;
        }
    }
}