using System;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class PlayerView : ITickable
    {
        [Inject] private readonly LazyInject<Player> _playerControl;
        [Inject] private readonly IUnitsProxy _unitsProxy;

        public Action<Unit> OnUnitSpotted;

        private Unit _currSpottedUnit = null;
        private float _viewDist;

        [Inject]
        public void InitStats(StatsScriptable[] stats)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].ID == "player")
                {
                    _viewDist = stats[i].GetStatAmount(StatType.lookDist);
                }
            }
        }

        public void Tick()
        {
            if (_playerControl.Value == null)
            {
                return;
            }
            var unitsOnPlanet = _unitsProxy.GetUnits(_playerControl.Value.Move.Round);

            float bestDist = _viewDist;
            Unit bestUnit = null;

            Vector2 playerPos = _playerControl.Value.Move.Position;

            for (int i = 0; i < unitsOnPlanet.Count; i++)
            {
                Vector2 unitPos = unitsOnPlanet[i].Move.Position;
                if (UnitUtils.IsUnitLookTo(_playerControl.Value, unitPos))
                {
                    float dist = (unitPos - playerPos).magnitude;
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        bestUnit = unitsOnPlanet[i];
                    }
                }
            }

            if (bestUnit != null)
            {
                if (_currSpottedUnit != bestUnit)
                {
                    _currSpottedUnit = bestUnit;
                    OnUnitSpotted?.Invoke(bestUnit);
                }
            }
            else if (_currSpottedUnit != null)
            {
                OnUnitSpotted?.Invoke(null);
                _currSpottedUnit = null;
            }
        }
    }
}