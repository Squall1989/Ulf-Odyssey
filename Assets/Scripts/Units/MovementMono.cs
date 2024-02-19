using UnityEngine;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        protected CircleMove _circleMove;

        public CircleMove CircleMove => _circleMove;

        public virtual void Init(Planet planet, CircleMove circleMove, float angle)
        {
            _circleMove = circleMove;
            _circleMove.ToLand(planet, angle);
            _circleMove.SetMoveDirect(0);
            transform.position = _circleMove.Position;
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