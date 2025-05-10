using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ulf
{
    public class ShootMono : MonoBehaviour
    {
        [SerializeField] private Transform aimTransform;
        [SerializeField] private ActionsMono actionMono;
        [SerializeField] private ProjectileMono missilePrefab;
        [SerializeField] private LayerMask targetMask;

        private Quaternion _startRotation;

        public void Start()
        {
            _startRotation = aimTransform.localRotation;
            actionMono.Action.OnAction += ActionReceive;
        }

        private void ActionReceive(ActionType type, int param)
        {
            if (type == ActionType.aim)
            {
                Aim(param);
            }
        }

        public void Aim(float angle)
        {
            aimTransform.localRotation = _startRotation;
            aimTransform.Rotate(new Vector3(0,0, angle));
        }

        public void Shoot()
        {
            var missile = Instantiate(missilePrefab, null);
            missile.transform.position = aimTransform.position;
            missile.transform.rotation = aimTransform.rotation;
            missile.Launch(targetMask, -transform.up);
        }
    }
}