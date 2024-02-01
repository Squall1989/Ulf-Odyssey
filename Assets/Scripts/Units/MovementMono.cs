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
            _circleMove.ToLand(planet.Position, planet.Radius, angle);
            _circleMove.SetMoveDirect(0);
            transform.position = _circleMove.Position;
        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
            transform.position = _circleMove.Position;
            LookAtPlanet(transform, _circleMove.PlanetPosition);
        }

        public static void LookAtPlanet(Transform transform, Vector3 planetPos)
        {
            Vector3 relative = -transform.InverseTransformPoint(planetPos);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);
        }
    }
}