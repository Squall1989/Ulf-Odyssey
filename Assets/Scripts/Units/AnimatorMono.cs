using UnityEngine;

namespace Ulf
{
    public class AnimatorMono : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetSpeed(float speed)
        {
            animator.SetFloat("speed", speed / 10);
        }
    }
}