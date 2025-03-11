using System;

namespace Ulf
{
    public class ActionUnit : IKillable
    {
        private ActionType _currAction = ActionType.none;

        public ActionType CurrentAction => _currAction;
        public bool IsActionInProcess => _currAction > ActionType.none;
         
        public Action<(int damage, int attackerGuid)> OnAttacked;
        public Action<ActionType, int> OnAction;
        private bool _isDead = false;
        private int _guid;

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
        private void AttackedFrom(int damage, int attackerGuid)
        {
            OnAttacked?.Invoke((damage, _guid));
        }
    }
}