using Assets.Scripts.Interfaces;

using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Bridge : Planet
    {
        private float startDeg, endDeg;

        public Bridge(Vector2 pos, float radius, float startDeg, float endDeg) : base( radius, default, pos)
        {
            this.startDeg = startDeg;
            this.endDeg = endDeg;
        }
    }
}