using System;
using UnityEngine;

namespace Ulf
{
    public class TowerBuildMono : BuildMono
    {
        [SerializeField]
        private Transform unitPlatform;

        public Transform UnitPlatform => unitPlatform;

        internal void InvokeAction(ActionUnit action)
        {

        }
    }
}