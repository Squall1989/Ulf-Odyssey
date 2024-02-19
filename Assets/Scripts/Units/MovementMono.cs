using System;
using UnityEngine;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        protected CircleMove _circleMove;
        private Transform _visualTransform;

        public CircleMove CircleMove => _circleMove;

        public virtual void Init(Planet planet, CircleMove circleMove, float angle, Transform visualTransform)
        {
            _circleMove = circleMove;
            _circleMove.ToLand(planet, angle);
            _circleMove.SetMoveDirect(0);
            _visualTransform = visualTransform;
            transform.position = _circleMove.Position;

            circleMove.OnMoveDirect += ChangeDirect;
        }

        private void ChangeDirect(int direct)
        {
            if(direct == -1)
            {
                _visualTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direct == 1)
            {
                _visualTransform.localRotation = Quaternion.Euler(0, 180f, 0);
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