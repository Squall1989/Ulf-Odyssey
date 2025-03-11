using System;
using System.Collections;
using UnityEngine;

namespace Ulf
{
    public class PlayerMovementMono : MovementMono
    {
        protected ExtendedCircleMove extendedCircleMove;
        private Coroutine directCorout;

        private int _standDirect;

        private void Start()
        {
            StartCoroutine(StandDirectCorout());
        }

        public override void Init(Planet planet, CircleMove circleMove, float angle)
        {
            base.Init(planet, circleMove, angle);
            SetEnemyLayerMask(LayerMask.GetMask("unit"));

            extendedCircleMove = circleMove as ExtendedCircleMove;
        }

        public void ControlStandDirect(int direct)
        {
            if (direct == 0)
            {
                if (directCorout != null)
                    StopCoroutine(directCorout);

                directCorout = StartCoroutine(StandDirectDelayCorout(2f, direct));
            }
            else
            {
                _standDirect = direct;
            }
        }

        protected IEnumerator StandDirectCorout()
        {
            while (true)
            {
                extendedCircleMove.CheckStandOpportunity(_standDirect);
                yield return null;
            }
        }

        protected IEnumerator StandDirectDelayCorout(float delay, int direct)
        {
            yield return new WaitForSeconds(delay);
            _standDirect = direct;

            directCorout = null;
        }

        protected override float RotateUnit()
        {
            float rotateMeasure = base.RotateUnit();

            if(MathF.Abs(rotateMeasure) > 3f)
            {
                rotateMeasure /= 10f;
            }

            return rotateMeasure;
        }
    }
}