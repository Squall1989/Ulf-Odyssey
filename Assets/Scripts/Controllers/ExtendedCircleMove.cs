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

        public Action<string> OnLog;
        public Action<int, float> OnRoundStand;

        public ExtendedCircleMove(float speed) : base(speed)
        {
        }

        protected override void Move(int moveDirect)
        {
            if(_isOnBridge)
            {
                moveDirect *= -1;
            }
            base.Move(moveDirect);

            //OnLog?.Invoke("angle: " + currDegree);
        }

        public void ToLand(IRound round, float startAngle, bool isOnBridge)
        {
            base.ToLand(round, startAngle);

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
                var bridgeDegree = GetRelativeBridgeDeg();
                if (_bridgeToStand.IsStandableBridgeDegree(bridgeDegree, true))
                {
                    var degree = GetAngle(Position - (Vector2)_bridgeToStand.RoundMono.TransformRound.position);
                    ToLand(_bridgeToStand, degree, true);
                    OnRoundStand?.Invoke(_bridgeToStand.ID, degree);
                }
            }
        }



        private float GetRelativeBridgeDeg()
        {
            var relative = (Vector2)_bridgeToStand.RoundMono.TransformRound.InverseTransformPoint(Position);
            var bridgeDegree = GetAngle(relative);
            return bridgeDegree;
        }

        private void TryLeaveBridge()
        {
            if(_isOnBridge)
            {
                var bridgeDegree = GetRelativeBridgeDeg();
                if (_bridgeToStand.IsStandableBridgeDegree(bridgeDegree, false))
                {
                    Planet planet = (_round as Bridge).GetOutPlanet(bridgeDegree);
                    if (planet == null)
                    {
                        OnLog?.Invoke("planet is null, return " );
                        return;
                    }

                    var planetDegree = GetAngle(Position - (Vector2)planet.RoundMono.TransformRound.position);

                    ToLand(planet, planetDegree, false);
                    OnRoundStand?.Invoke(planet.ID, planetDegree);
                }
            }
        }


    }
}