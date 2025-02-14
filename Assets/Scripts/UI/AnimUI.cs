using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Ulf
{
    public class AnimUI : MonoBehaviour
    {
        [SerializeField] private RectTransform scroll1;
        [SerializeField] private float scroll1H, scroll1L;

        [SerializeField] private Image part1;
        [SerializeField] private Image part2;
        [SerializeField] private RectTransform target;
        [SerializeField] private Material uiBlurMat;

        [SerializeField] private float delayFwd;
        [SerializeField] private float delayBack;
        [SerializeField] private float moveTime;
        [SerializeField] private float yMove;
        [SerializeField] private float zRotate;
        [SerializeField] private float rotateTime;
        [SerializeField] private Vector2 blurPeak;

        private Vector2 startPos;
        private Quaternion startRot;
        private Quaternion endRot;

        private float scrollWidth;

        void Start()
        {
            scrollWidth = scroll1.rect.width;
            startPos = target.anchoredPosition;
            endRot = target.localRotation * Quaternion.Euler(0, 0, -2);
            // Little bit rotate for clock-wise ending
            startRot = target.localRotation * Quaternion.Euler(0,0, 2);
        }

        private void FwdAnim()
        {
            target.DOAnchorPos(startPos - new Vector2(0, yMove), moveTime);
            part1.DOFade(0, 0);
            DOTween.Sequence()
                .AppendInterval(delayFwd)
                .Append(target.DORotateQuaternion(Quaternion.identity, rotateTime).OnComplete(() =>
                {
                    ScrollAnim();
                    isFwdAnim = false;
                }))
                .Play();
            DOTween.Sequence()
                .AppendInterval(.25f)
                .Append(part1.DOFade(1f, .1f));

        }

        private void ScrollAnim()
        {
            Vector2 size = new Vector2(scrollWidth, 
                isFwdAnim ? scroll1H : scroll1L);
            scroll1.DOSizeDelta(size, .8f);
        }

        private void ReverseAnim()
        {
            ScrollAnim();
            target.DORotateQuaternion(startRot, rotateTime);
            DOTween.Sequence()
                .AppendInterval(rotateTime - delayBack)
                .Append(target.DOAnchorPos(startPos, moveTime).OnComplete(() =>
                {
                    isFwdAnim = true;
                }))
                .Append(target.DORotateQuaternion(endRot, rotateTime));
            DOTween.Sequence()
             .AppendInterval(delayBack)
             .Append(part2.DOFade(0f, 0))
             .AppendInterval(rotateTime)
             .Append(part2.DOFade(1, 0));
        }

        private void Blur()
        {
            float blurTime = rotateTime + moveTime;
            uiBlurMat.DOVector(blurPeak, "_BlurPower", blurTime / 2f).OnComplete(() =>
            {
                uiBlurMat.DOVector(Vector2.zero, "_BlurPower", blurTime / 2f);
            });
        }

        bool isFwdAnim = true;
        public void StartAnim()
        {
            if (isFwdAnim)
            {
                FwdAnim();
            }
            else
            {
                ReverseAnim();
            }

            Blur();
        }
    }

}