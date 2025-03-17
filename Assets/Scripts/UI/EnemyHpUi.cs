using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ulf
{
    public class EnemyHpUi : MonoBehaviour
    {
        [SerializeField] private Image[] hpImages;
        [SerializeField] private RectTransform[] oddRects;
        [Inject] private ElementSpritesScriptable sprites;

        private Vector2[] evenPoses;
        private Vector2[] oddPoses;

        private bool isEvenPoses = true;

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

            isEvenPoses = startCount % 2 == 0;

            SetPoses(startCount);
            SetSprites(startCount, element);
        }

        private void SetSprites(int startCount, ElementType element)
        {
            Sprite currSprite = sprites.GetByElement(element);
            for(int i = 0;i < startCount; i++)
            {
                hpImages[i].sprite = currSprite;
            }
        }

        private void SetPoses(int startCount)
        {
            Vector2[] curArray = isEvenPoses ? evenPoses : oddPoses;

            for (int i = 0; i < hpImages.Length; i++)
            {
                hpImages[i].enabled = i < startCount;
                if(curArray.Length > i)
                    hpImages[i].transform.localPosition = curArray[i];
            }
            
        }

        internal void ChangeHealth(int hp)
        {
            for(int i = 0; i < hpImages.Length; i++)
            {
                hpImages[i].enabled = hp > i;
            }
        }
    }
}