using Vector2 = UnityEngine.Vector2;
using System;
using UnityEditor.Build;

namespace Ulf
{
    public class ExtendedCircleMove : CircleMove
    {
        private bool _isOnBridge;
        private Bridge _bridgeToStand;
        private int _standDirect;

        public ExtendedCircleMove(float speed) : base(speed)
        {
        }

        public void ToLand(IRound round, Vector2 pos, float radius, float startAngle, bool isOnBridge)
        {
            base.ToLand(round, pos, radius, startAngle);

            _isOnBridge = isOnBridge;
        }

        internal void SetBridge(Bridge bridge)
        {
            this._bridgeToStand = bridge;
        }

        internal void SetStandDirect(int direction)
        {
            _standDirect = direction;

            if(_standDirect != 0)
            {
                CheckStandOpportunity();
            }
        }

        private void CheckStandOpportunity()
        {
            if (_isOnBridge)
            {
                if(_standDirect == -1)
                    TryLeaveBridge();
            }
            else
            {
                if(_standDirect == 1)
                    TryStandBridge();
            }
        }

        private void TryStandBridge()
        {
            if(_bridgeToStand != null)
            {
                var degree = GetAngle(_bridgeToStand.Position, Position);
                if (IsStandableBridgeDegree(degree))
                {
                    ToLand(_bridgeToStand, _bridgeToStand.Position, _bridgeToStand.Size, degree, true);
                }
            }
        }

        private bool IsStandableBridgeDegree(float degree)
        {
            if (degree < 90 || degree > 270)
                return true;
            else 
                return false;
        }

        private void TryLeaveBridge()
        {
            if(_isOnBridge)
            {
                var degree = GetAngle(_bridgeToStand.Position, Position);
                if (IsStandableBridgeDegree(degree))
                {
                    Planet planet = _bridgeToStand.GetOutPlanet(degree);
                    ToLand(planet, planet.Position, planet.Radius, degree, true);
                }
            }
        }
    }
}