using UnityEngine;

namespace Ulf
{
    public class WeaponMono : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            UnityEngine.Debug.Log("Weapon trigger: " + collision.gameObject.name);
        }
    }
}