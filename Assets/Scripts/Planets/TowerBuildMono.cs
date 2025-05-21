using System;
using UnityEngine;

namespace Ulf
{
    public class TowerBuildMono : BuildMono
    {
        [SerializeField]
        private Transform unitPlatform;
        [SerializeField]
        private TrapMono trap;

        private ActionUnit _unitAction;

        public Transform UnitPlatform => unitPlatform;


        private void Awake()
        {
            trap.OnDamage += DamageUnit;
        }

        private void DamageUnit(Unit unit, int dmg)
        {
            _unitAction.AttackUnit(unit, dmg);
        }

        internal void InvokeAction(ActionUnit action)
        {
            trap.gameObject.SetActive(true);
            _unitAction = action;
        }
    }
}