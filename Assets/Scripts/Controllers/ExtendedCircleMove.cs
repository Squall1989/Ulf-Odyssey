
using UnityEngine;

namespace Ulf
{
    public class ExtendedCircleMove : CircleMove
    {
        private bool _isReversed;

        public ExtendedCircleMove(float speed) : base(speed)
        {
        }

        public void ToLand(Vector2 pos, float radius, float startAngle, bool isReversed)
        {
            base.ToLand(pos, radius, startAngle);

            _isReversed = isReversed;
        }
    }
}