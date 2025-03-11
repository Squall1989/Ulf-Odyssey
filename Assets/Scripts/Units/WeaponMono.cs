using System;
using UnityEngine;

namespace Ulf
{
    public class WeaponMono : MonoBehaviour
    {
        public Action<Unit, int> OnUnitTriggered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var unit = collision.GetComponent<UnitMono>();
            if (unit != null)
            {
                UnityEngine.Debug.Log("Unit damaged: " +  unit.name);
                OnUnitTriggered?.Invoke(unit.Unit, 1);
            }
        }
    }
}