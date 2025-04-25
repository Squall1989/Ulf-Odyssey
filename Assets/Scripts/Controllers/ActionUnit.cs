using System;
using System.Collections;
using System.Threading.Tasks;

namespace Ulf
{
    public class ActionUnit : IKillable
    {
        private ActionType _currAction = ActionType.none;

        public ActionType CurrentAction => _currAction;
        public bool IsActionInProcess => _currAction > ActionType.none;
         
        public Action<(int damage, int attacked, int attacker)> OnAttacked;
        public Action<ActionType, int> OnAction;
        private bool _isDead = false;
        private int _guid;

        private (int guid, float time) _attackLast;

        public int Attacker => _attackLast.time > 0 ? _attackLast.guid : -1;

        public ActionUnit(int guid)
        {
            _guid = guid;
        }

        internal void UniversalAction(ActionType actionType, int paramNum)
        {
            if(_isDead)
            {
                return;
            }
            OnAction?.Invoke(actionType, paramNum);
            _currAction = actionType;
        }

        public void EndAction()
        {
            _currAction = ActionType.none;
        }

        public void Kill()
        {
            _isDead = true;
        }

        public void Ressurect()
        {
            _isDead = false;
        }

        internal void AttackUnit(Unit unit, int damage)
        {
            unit.Actions.AttackedFrom(damage, _guid);
        }
        /// <summary>
        /// ToDo: aggression back for units
        /// </summary>
        internal void AttackedFrom(int damage, int attackerGuid)
        {
            OnAttacked?.Invoke((damage, _guid, attackerGuid));
            _attackLast = new(attackerGuid, 1f);
            AttackedTimer();
        }

        private async void AttackedTimer()
        {
            while (_attackLast.time > 0)
            {
                _attackLast.time -= .1f;
                await Task.Delay(100);
            }
        }
    }
}