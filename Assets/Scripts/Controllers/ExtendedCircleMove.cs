using Vector2 = UnityEngine.Vector2;
using System;

namespace Ulf
{
    public class ExtendedCircleMove : CircleMove
    {
        private bool _isOnBridge;
        private Bridge _bridgeToStand;
        private int standDirect;

        public ExtendedCircleMove(float speed) : base(speed)
        {
        }

        public void ToLand(Vector2 pos, float radius, float startAngle, bool isOnBridge)
        {
            base.ToLand(pos, radius, startAngle);

            _isOnBridge = isOnBridge;
        }

        internal void SetBridge(Bridge bridge)
        {
            this._bridgeToStand = bridge;
        }

        internal void SetStandDirect(int direction)
        {
            standDirect = direction;

            if(standDirect != 0)
            {
                CheckStandOpportunity();
            }
        }

        private void CheckStandOpportunity()
        {
            if (_isOnBridge)
            {

            }
        }
    }
}