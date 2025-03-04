using System;
using UnityEngine;

namespace Ulf
{

    public class MovementMono : MonoBehaviour
    {
        protected CircleMove _circleMove;
        [SerializeField] private Transform _visualTransform;

        private Vector3 leftDir, rightDir;

        public CircleMove CircleMove => _circleMove;

        public virtual void Init(Planet planet, CircleMove circleMove, float angle)
        {
            _circleMove = circleMove;
            _circleMove.ToLand(planet, angle);
            _circleMove.SetMoveDirect(0);
            transform.position = _circleMove.Position;

            circleMove.OnMoveDirect += ChangeDirect;

            rightDir = _visualTransform.localRotation.eulerAngles;
            leftDir = rightDir + new Vector3(0, 180f, 0);
        }

        private void ChangeDirect(int direct)
        {
            if(direct == -1)
            {
                _visualTransform.localRotation = Quaternion.Euler(leftDir);
            }
            else if (direct == 1)
            {
                _visualTransform.localRotation = Quaternion.Euler(rightDir);
            }
        }

        protected virtual float RotateUnit()
        {
           return _circleMove.Round.RoundMono.LookAtCenter(transform);

        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
            transform.position = _circleMove.Position;
            transform.Rotate(new Vector3(0,0,RotateUnit()));
        }

    }
}