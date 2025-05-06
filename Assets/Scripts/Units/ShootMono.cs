using UnityEngine;

namespace Ulf
{
    public class ShootMono : MonoBehaviour
    {
        [SerializeField] private Transform aimTransform;

        private Quaternion _startRotation;

        public void Start()
        {
            _startRotation = aimTransform.rotation;
        }

        public void Aim(float angle)
        {
            aimTransform.rotation = _startRotation;
            aimTransform.Rotate(new Vector3(0,0, angle));
        }
    }
}