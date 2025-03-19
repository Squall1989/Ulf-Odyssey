using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ulf
{

    public class EnemyHpUi : MonoBehaviour
    {
        [SerializeField] private PiecesPool poolPieces;
        [SerializeField] private PiecesAnimStatic _animPieces;
        [SerializeField] private Image[] hpImages;
        [SerializeField] private RectTransform[] oddRects;
        [Inject] private ElementSpritesScriptable sprites;

        private Vector2[] evenPoses;
        private Vector2[] oddPoses;
        private ElementType _currElement;
        private int _currHp;
        private bool isEvenPoses = true;
        private Vector2[] _currPoses;

        private void Awake()
        {
            _animPieces.Init(poolPieces);

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
            _currElement = unitHealth.Element;
            _currHp = unitHealth.CurrHealth;
            isEvenPoses = startCount % 2 == 0;

            SetPoses(startCount);
            SetSprites(startCount, _currElement);
        }

        private void SetSprites(int startCount, ElementType element)
        {
            Sprite currSprite = sprites.GetByElement(element, true);
            for(int i = 0;i < startCount; i++)
            {
                hpImages[i].sprite = currSprite;
            }
        }

        private void SetPoses(int startCount)
        {
            _currPoses = isEvenPoses ? evenPoses : oddPoses;

            for (int i = 0; i < hpImages.Length; i++)
            {
                hpImages[i].enabled = i < startCount;
                if(_currPoses.Length > i)
                    hpImages[i].transform.localPosition = _currPoses[i];
            }
        }

        internal void ChangeHealth(int hp)
        {
            for(int i = _currHp -1; i >= hp; i--)
            {
                hpImages[i].sprite = sprites.GetByElement(_currElement, false);
                _animPieces.HealthDestroy(_currPoses[i], _currElement);
            }
            _currHp = hp;
        }
    }
}