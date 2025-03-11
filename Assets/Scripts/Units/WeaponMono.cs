using System;
using UnityEngine;

namespace Ulf
{
    public class WeaponMono : MonoBehaviour
    {
        public Action<Unit, int> OnUnitTriggered;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        internal void Activate(bool isEnable)
        {
            _collider.enabled = isEnable;
        }

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