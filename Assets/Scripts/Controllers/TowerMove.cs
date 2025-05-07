using Unity.Mathematics;

namespace Ulf
{
    public class TowerMove : CircleMove
    {
        private float2 _platformPos;

        public void TowerStand(float2 pos)
        {
            _platformPos = pos;
        }

        protected override void Move(int moveDirect)
        {
            _position = _platformPos;
        }
    }
}
