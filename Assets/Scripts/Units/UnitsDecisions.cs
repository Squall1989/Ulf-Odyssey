using System.Collections.Generic;
using Zenject;
using System;
using System.Linq;
using UnityEngine;

namespace Ulf
{
    public class UnitsDecisions : IUnitsProxy, ITickable
    {
        protected List<UnitBrain> unitBrains = new();
        protected List<Unit> _units = new();

        public Action<int, INextAction> OnUnitAction;
        [Inject]
        private readonly IPlayersProxy _playersProxy;
        [Inject]
        private readonly BehaviourScriptable[] _behaviours;
        [Inject]
        private readonly StatsScriptable[] _stats;

        private int nextBrainNum = 0;

        public void Add(Unit unit)
        {
            var unitStats = _stats.FirstOrDefault(p => p.ID == unit.View);
            var unitBehaviour = _behaviours.FirstOrDefault(p => p.behavId == unit.View);
            
            UnitBrain unitBrain = new UnitBrain(_playersProxy, unit, unitStats, unitBehaviour);
            unitBrain.OnUnitMove += CreateMovement;
            unitBrain.OnUnitAction += CreateAction;
            
            unitBrains.Add(unitBrain);
            _units.Add(unit);
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

        public void CreateDamage((int amount, int attacked, int attacker) param)
        {
            DamageAction damage = new DamageAction()
            {
                damageAmount = param.amount,
                damager = param.attacker,
            };

            Unit unit = _units.FirstOrDefault(p => p.GUID == param.attacked);

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

        public List<Unit> GetUnits(IRound round)
        {
            return _units.Where(p => p.Move.Round == round).ToList();
        }

        public List<Unit> GetUnits()
        {
            return _units;
        }
    }


}