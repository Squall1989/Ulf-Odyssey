using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        private CircleMove _circleMove;

        public CircleMove CircleMove => _circleMove;

        public void Init(Planet planet, CircleMove circleMove, float angle)
        {
            _circleMove = circleMove;
            _circleMove.ToLand(planet, angle);
            _circleMove.SetMoveDirect(0);
            transform.position = _circleMove.Position;
        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
            transform.position = _circleMove.Position;
            _circleMove.Round.RoundMono.LookAtCenter(transform); 
        }

    }
}