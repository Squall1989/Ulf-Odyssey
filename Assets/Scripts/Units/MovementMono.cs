using UnityEngine;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        private CircleMove _circleMove;

        public void Init(Planet planet, CreateUnitStruct unitStruct, (float angleFrom, float angleTo) freeArc)
        {
            _circleMove = new(unitStruct.MoveSpeed);
            float startAngle = new System.Random().Next((int)freeArc.angleFrom, (int)freeArc.angleTo);
            _circleMove.ToLand(planet.Position, planet.Radius, startAngle);
            _circleMove.Move(0);
            transform.position = _circleMove.Position;
        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
        }
    }
}