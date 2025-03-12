using System.Collections.Generic;
using Zenject;
using System;
using System.Linq;
using UnityEngine;
using MsgPck;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

namespace Ulf
{
    public class UnitsDecisions : IUnitsProxy, ITickable
    {
        protected List<UnitBrain> unitBrains = new();
        protected List<Unit> units = new();

        public Action<int, INextAction> OnUnitAction;
        private readonly IPlayersProxy _playersProxy;
        private readonly BehaviourScriptable[] _behaviours;
        private readonly StatsScriptable[] _stats;

        private int nextBrainNum = 0;

        public UnitsDecisions(StatsScriptable[] stats, BehaviourScriptable[] behaviours, IPlayersProxy playersProxy)
        {
            _playersProxy = playersProxy;
            _behaviours = behaviours;
            _stats = stats;
        }

        public void Add(Unit unit)
        {
            var unitStats = _stats.FirstOrDefault(p => p.ID == unit.View);
            var unitBehaviour = _behaviours.FirstOrDefault(p => p.behavId == unit.View);
            
            UnitBrain unitBrain = new UnitBrain(_playersProxy, unit, unitStats, unitBehaviour);
            unitBrain.OnUnitMove += CreateMovement;
            unitBrain.OnUnitAction += CreateAction;
            
            unitBrains.Add(unitBrain);
            units.Add(unit);
            unit.Actions.OnAttacked += CreateDamage;
        }

        private void CreateMovement(Unit unit, int dir, float speed)
        {
            var move = new MovementAction() { 
                direction = dir,
                fromAngle = unit.Degree,
                speed = speed,
            };

            move.DoAction(unit);

            OnUnitAction?.Invoke(unit.GUID, move);
        }

        private void CreateAction(Unit unit, ActionType actionType, int param)
        {
            var action = new UniversalAction()
            {
                 action = actionType,
                 paramNum = param,
            };

            action.DoAction(unit);

            OnUnitAction?.Invoke(unit.GUID, action);
        }

        public void CreateDamage((int amount, int guid) param)
        {
            DamageAction damage = new DamageAction()
            {
                damageAmount = param.amount,
            };

            Unit unit = units.FirstOrDefault(p => p.GUID == param.guid);

            damage.DoAction(unit);

            OnUnitAction?.Invoke(unit.GUID, damage);
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            unitBrains[nextBrainNum].MakeDecision(deltaTime);

            if (++nextBrainNum >= unitBrains.Count)
            {
                nextBrainNum = 0;
            }
        }
    }


}