using UnityEngine;
using UnityEngine.UI;

namespace Ulf
{
    public class EnemyHpUi : MonoBehaviour
    {
        [SerializeField] private Image[] hpImages;
        [SerializeField] private RectTransform[] oddRects;

        private Vector2[] evenPoses;
        private Vector2[] oddPoses;

        private void Awake()
        {
            evenPoses = new Vector2[hpImages.Length];
            oddPoses = new Vector2[oddRects.Length];

            for (int i = 0; i < hpImages.Length; i++)
            {
                evenPoses[i] = hpImages[i].transform.localPosition;
            }

            for (int i = 0; i < oddRects.Length; i++)
            {
                oddPoses[i] = oddRects[i].localPosition;
            }
        }

        public void InitHealth(Health unitHealth)
        {
            int startCount = unitHealth.CurrHealth;
            ElementType element = unitHealth.Element;


        }
    }
}