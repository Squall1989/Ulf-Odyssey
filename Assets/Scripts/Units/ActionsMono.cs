using UnityEngine;

namespace Ulf
{

    public class ActionsMono : MonoBehaviour, IKillable
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected WeaponMono[] _attackColliders;
        private ActionUnit _action;

        public ActionUnit Action => _action;

        protected void Awake()
        {

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
            if (_action.CurrentAction == ActionType.attack)
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

            _attackColliders[num].Activate(isEnable);
        }

        internal void Init(ActionUnit actionUnit)
        {
            _action = actionUnit;
            _action.OnAction += ActionUniversal;

            for (int i = 0; i < _attackColliders.Length; i++)
            {
                _attackColliders[i].OnUnitTriggered += _action.AttackUnit;
                DeActivateCollider(i);
            }
        }

        private void ActionUniversal(ActionType type, int param)
        {
            if(_action.IsActionInProcess)
            {
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
        public void Kill()
        {
            _animator.SetInteger("death", 1);
            _animator.SetInteger("attack", 0);

        }

        public void Ressurect()
        {
            _animator.SetInteger("death", 0);
        }
    }
}