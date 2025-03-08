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

        public Action<int, INextAction> OnUnitAction;
        private readonly IPlayersProxy _playersProxy;
        private readonly StatsScriptable[] _stats;

        private int nextBrainNum = 0;

        public UnitsDecisions(StatsScriptable[] stats, IPlayersProxy playersProxy)
        {
            _playersProxy = playersProxy;
            _stats = stats;
        }

        public void Add(Unit unit)
        {
            var unitStats = _stats.FirstOrDefault(p => p.ID == unit.View);
            
            UnitBrain unitBrain = new UnitBrain(_playersProxy, unit, unitStats);
            unitBrain.OnUnitMove += CreateMovement;
            
            unitBrains.Add(unitBrain);
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            unitBrains[nextBrainNum].MakeDecision(deltaTime);

            if(++nextBrainNum >= unitBrains.Count)
            {
                nextBrainNum = 0;
            }
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

    }


}