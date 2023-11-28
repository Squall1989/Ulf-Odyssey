using UnityEngine;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        private CircleMove _circleMove;

        public CircleMove CircleMove => _circleMove;

        public void Init(Planet planet, DefaultUnitStruct unitStruct, float angle)
        {
            _circleMove = new(unitStruct.MoveSpeed);
            _circleMove.ToLand(planet.Position, planet.Radius, angle);
            _circleMove.SetMoveDirect(0);
            transform.position = _circleMove.Position;
        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
            transform.position = _circleMove.Position;
        }
    }
}