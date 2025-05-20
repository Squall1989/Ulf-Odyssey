using System;
using UnityEngine;

namespace Ulf
{
    public class TrapMono : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _targetMask;


        public Action<Unit, int> OnDamage;


        private void Awake()
        {

        }

        public void DeactivateCollider()
        {
            _collider.enabled = false;
            gameObject.SetActive(false);
        }

        public void ActivateCollider()
        {
            _collider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int layer = collision.gameObject.layer;
            bool isTarget = ((1 << layer) & _targetMask.value) != 0;

            if (isTarget)
            {
                if (isTarget)
                {
                    UnitMono unit = collision.gameObject.GetComponent<UnitMono>();
                    OnDamage?.Invoke(unit.Unit, 1);
                }
            }
        }
    }
}