using UnityEngine;

namespace Ulf
{
    public class MovementMono : MonoBehaviour
    {
        private CircleMove _circleMove;
        public CircleMove CircleMove => _circleMove;

        public void Init(Planet planet, CreateUnitStruct unitStruct, (float angleFrom, float angleTo) freeArc)
        {
            _circleMove = new(unitStruct.MoveSpeed);
            float startAngle = new System.Random().Next((int)freeArc.angleFrom, (int)freeArc.angleTo);
            _circleMove.ToLand(planet.Radius, startAngle);
        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
        }
    }
}