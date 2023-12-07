using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Bridge : IRound
    {
        private float planetAngle;

        public float Size { get; private set; }

        public Bridge(float size)
        {
            Size = size;
        }

        public void AddUnit(Unit unit)
        {

        }

        public void RmUnit(Unit unit)
        {

        }
    }
}