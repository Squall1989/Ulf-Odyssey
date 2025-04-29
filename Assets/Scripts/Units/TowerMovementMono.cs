using UnityEngine;

namespace Ulf
{
    public class TowerMovementMono : MovementMono
    {
        private Transform _platform;

        public void StandTower(Transform platform)
        {
            _platform = platform;
        }

        protected override void Update()
        {
            base.Update();
            transform.position = _platform.position;
        }
    }
}