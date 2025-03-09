using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Ulf
{

    public class ActionsMono : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected WeaponMono[] _attackColliders;
        private ActionUnit _action;

        protected void Awake()
        {
            for (int i = 0; i < _attackColliders.Length; i++)
            {
                DeActivateCollider(i);
            }
        }

        // From animation event
        public void DeActivateCollider(int num)
        {
            EnableCollider(num, false);
        }

        public void ActivateCollider(int num)
        {
            EnableCollider(num, true);
        }

        public void ActionAnimEnd()
        {
            var currAction = _action.CurrentAction;
            if (currAction == ActionType.attack)
            {
                _animator.SetInteger("attack", 0);
            }
            _action.EndAction();
        }

        private void EnableCollider(int num, bool isEnable)
        {
            if (num < 0 || num >= _attackColliders.Length)
            {
                UnityEngine.Debug.LogError("Wrong collider number!");
                return;
            }

            _attackColliders[num].gameObject.SetActive(isEnable);
        }

        internal void Init(ActionUnit actionUnit)
        {
            _action = actionUnit;
            _action.OnAction += ActionUniversal;
        }

        private void ActionUniversal(ActionType type, int param)
        {
            if(_action.IsActionInProcess)
            {
                UnityEngine.Debug.LogError("Action In Progress!!!");
                return;
            }
            switch(type)
            {
                case ActionType.attack:
                    _animator.SetInteger("attack", param);
                    break;
                case ActionType.death:
                    _animator.SetInteger("death", param);
                    break;
            }
        }


    }
}