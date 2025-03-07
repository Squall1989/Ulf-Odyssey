
using System.Collections.Generic;
using Zenject;
using Time = UnityEngine.Time;
using Random = UnityEngine.Random;
using MsgPck;
using System;
using System.Linq;

namespace Ulf
{

    public class UnitsDecisions : IUnitsProxy, ITickable
    {
        protected List<Unit> units = new();
        protected Dictionary<Unit, BehaviourUnitStruct> unitsBehaviourDict = new();

        public Action<int, INextAction> OnUnitAction;
        private readonly StatsScriptable[] _stats;
        private readonly UnitsBrain unitBrain;

        public UnitsDecisions(StatsScriptable[] stats, IPlayersProxy playersProxy)
        {
            _stats = stats;
            unitBrain = new UnitsBrain(playersProxy, stats);
        }

        public void Add(Unit unit)
        {
            units.Add(unit);
            ActionTime(unit);
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
            unitsBehaviourDict[unit] = GetNextAction(unit);
            unitsBehaviourDict[unit].prevAction.DoAction(unit);

            OnUnitAction?.Invoke(unit.GUID, unitsBehaviourDict[unit].prevAction);

        }

        private BehaviourUnitStruct GetNextAction(Unit unit)
        {
            int time = Random.Range(3, 10);
            var timer = new Timer(time);
            timer.OnTimeOver += () =>
            {
                ActionTime(unit);
            };

            var stat = _stats.FirstOrDefault(p => p.ID == unit.View);

            unitBrain.InitNextUnit(unit);
            MoveType move = unitBrain.DecideMove(unit, stat.GetStatAmount(StatType.lookDist));

            return new BehaviourUnitStruct()
            {
                prevAction = RandMovement(unit, stat, move),
                timer = timer,
            };
        }
            
        private MovementAction RandMovement(Unit unit, StatsScriptable stat, MoveType move)
        {

            // left[-1] right[+1] stay[0]
            int direct = Random.Range(-1, 2);
            float speed = 0;
            if (direct != 0)
            {
                speed = stat.GetStatAmount(move == MoveType.walk ? StatType.walkSpeed : StatType.runSpeed);
            }

            return new MovementAction() { 
                direction = direct,
                fromAngle = unit.Degree,
                speed = speed,
            };
        }

    }


}