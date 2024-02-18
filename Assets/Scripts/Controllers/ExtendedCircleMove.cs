using Vector2 = UnityEngine.Vector2;
using System;

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

            if (_isOnBridge)
            {
                var planet =_bridgeToStand.GetOutPlanet(GetRelativeBridgeDeg(), false);
                if(planet != null )
                {
                    LeaveToRound(planet);
                }
            }
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
                    LeaveToRound(_bridgeToStand);

                    OnLog?.Invoke("bridge in: " + _bridgeToStand.ID);
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
                    Planet planet = (_round as Bridge).GetOutPlanet(bridgeDegree, true);
                    if (planet == null)
                    {
                        OnLog?.Invoke("planet is null, return " );
                        return;
                    }
                    OnLog?.Invoke("planet out: " + planet.ID);

                    LeaveToRound(planet);
                }
            }
        }

        public void LeaveToRound(IRound round)
        {
            var planetDegree = GetAngle(Position - (Vector2)round.RoundMono.TransformRound.position);

            OnRoundStand?.Invoke(round.ID, planetDegree);
        }
    }
}