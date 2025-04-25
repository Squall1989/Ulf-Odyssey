using DG.Tweening;
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
        private int _maxHp;
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
            _currElement = unitHealth.Element;
            _currHp = unitHealth.CurrHealth;
            _maxHp = unitHealth.MaxHealth;
            isEvenPoses = _maxHp % 2 == 0;

            SetPoses();
            SetSprites();
        }

        private void SetSprites()
        {
            Sprite hpSprite = sprites.GetByElement(_currElement, true);
            Sprite backSprite = sprites.GetByElement(_currElement, false);
            for(int i = 0; i < _maxHp; i++)
            {
                if (i < _currHp)
                {
                    hpImages[i].sprite = hpSprite;
                    // Alpha set to zero
                    hpImages[i].color *= new Color(1, 1, 1, 0);
                    hpImages[i].DOFade(1, .2f);
                }
                else
                {
                    hpImages[i].sprite = backSprite;
                }
            }
        }

        private void SetPoses()
        {
            _currPoses = isEvenPoses ? evenPoses : oddPoses;

            for (int i = 0; i < hpImages.Length; i++)
            {
                hpImages[i].enabled = i < _maxHp;
                if(_currPoses.Length > i)
                    hpImages[i].transform.localPosition = _currPoses[i];
            }
        }

        internal void ChangeHealth(int hp)
        {
            for(int i = _currHp -1; i >= hp; i--)
            {
                hpImages[i].sprite = sprites.GetByElement(_currElement, false);
                _animPieces.HealthDestroy(_currPoses[i], _currElement, true);
            }
            _currHp = hp;
        }
    }
}