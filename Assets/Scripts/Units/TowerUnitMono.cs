using UnityEngine;

namespace Ulf
{
    public class TowerUnitMono : UnitMono
    {
        private Transform _platform;

        public void GoTower(TowerBuildMono tower)
        {
            _platform = tower.UnitPlatform;
            float standHeight = transform.InverseTransformPoint(_platform.position).y;
            _movement.SetMoveHeight(standHeight);
        }
    }
}