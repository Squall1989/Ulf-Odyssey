using UnityEngine;

namespace Ulf
{
    public class TowerBuildMono : BuildMono
    {
        [SerializeField]
        private Transform unitPlatform;

        public Transform UnitPlatform => unitPlatform;
    }
}