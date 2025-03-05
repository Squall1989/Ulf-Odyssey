using System;

namespace Ulf
{
    public class ActionUnit
    {
        private ActionType currAction = ActionType.none;

        public ActionType CurrentAction => currAction;
        public bool IsActionInProcess => currAction > ActionType.none;

        public Action<ActionType, int> OnAction;

        internal void UniversalAction(ActionType actionType, int paramNum)
        {
            OnAction?.Invoke(actionType, paramNum);
            currAction = actionType;
        }

        public void EndAction()
        {
            currAction = ActionType.none;
        }
    }
}