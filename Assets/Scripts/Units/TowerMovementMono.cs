using UnityEngine;

namespace Ulf
{
    public class TowerMovementMono : MovementMono
    {
        private Transform _platform;

        public void StandTower(Transform platform)
        {
            _platform = platform;
            (_circleMove as TowerMove).TowerStand((Vector2)platform.position);
        }


        public override void Init(Planet planet, float angle)
        {
            _circleMove = new TowerMove();
            base.Init(planet, angle);
        }
    }
}