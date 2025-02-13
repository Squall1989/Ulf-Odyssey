using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

namespace Ulf
{
    public class AnimUI : MonoBehaviour
    {
        [SerializeField] private List<AnimStep> stepList;
        [SerializeField] private RectTransform target;

        private int _step = 0;
        private AnimStep defaultPos;

        void Start()
        {
            defaultPos = CreateStep();
        }

        [ExecuteInEditMode]
        public void StartAnim()
        {
            if (defaultPos.Equals(default))
            {
                defaultPos = CreateStep();
            }

            NextAnimStep();
        }

        bool isAnimFwd = true;
        private void NextAnimStep()
        {
            if (_step < stepList.Count)
            {
                stepList[_step++].Execute(target, NextAnimStep);
            }
            else
            {
                isAnimFwd ^= true;
            }
        }

        public void SetRectTarget()
        {
            stepList.Add(CreateStep());
        }

        private AnimStep CreateStep()
        {
            return new AnimStep()
            {
                targetMove = target.position,
                targetRotate = target.localRotation,
                time = 1f,
            };
        }
    }

    [System.Serializable]
    public struct AnimStep
    {
        public float time;
        public Vector3 targetMove;
        public Quaternion targetRotate;

        public void Execute(RectTransform target, Action OnFinish)
        {
            bool isCompleteOne = false;
            target.DOLocalMove(targetMove, time).OnComplete(() =>
            {
                if(!isCompleteOne)
                {
                    isCompleteOne = true;
                }
                else
                {
                    OnFinish?.Invoke();
                }
            });
            target.DORotateQuaternion(targetRotate, time).OnComplete(() =>
            {
                if (!isCompleteOne)
                {
                    isCompleteOne = true;
                }
                else
                {
                    OnFinish?.Invoke();
                }
            });

        }
    }
}