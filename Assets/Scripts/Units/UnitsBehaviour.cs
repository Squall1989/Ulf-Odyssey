
using System.Collections.Generic;
using Zenject;
using Time = UnityEngine.Time;
using Random = UnityEngine.Random;
using MsgPck;
using System;

namespace Ulf
{
    public class UnitsBehaviour : IUnitsProxy, ITickable
    {
        protected List<Unit> units = new();
        protected Dictionary<Unit, BehaviourUnitStruct> unitsBehaviourDict = new();

        public Action<int, INextAction> OnUnitAction;

        public UnitsBehaviour() 
        {

        }

        public void Add(Unit unit)
        {
            units.Add(unit);
            unitsBehaviourDict.Add(unit, GetNextAction(unit));
        }

        public void Tick()
        {
            foreach (Unit unit in units)
            {
                unitsBehaviourDict[unit].timer.RemainingTime -= Time.deltaTime;
            }
        }

        private void ActionTime(Unit unit)
        {
            OnUnitAction?.Invoke(unit.GUID, unitsBehaviourDict[unit].nextAction);

            unitsBehaviourDict[unit].nextAction.DoAction(unit);
            unitsBehaviourDict[unit] = GetNextAction(unit);
        }

        private BehaviourUnitStruct GetNextAction(Unit unit)
        {
            // left[-1] right[+1] stay[0]
            int direct = Random.Range(-1, 2);

            int time = Random.Range(3, 10);
            var timer = new Timer(time);
            timer.OnTimeOver += () =>
            {
                ActionTime(unit);
            };

            return new BehaviourUnitStruct()
            {
                nextAction = new MovementAction() { direction = direct },
                timer = timer,
            };
        }
            
    }


}