using System;
using Unity.Entities.UniversalDelegates;
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

        public void Tick()
        {
            if (_playerControl.Value == null)
            {
                return;
            }
            var unitsOnPlanet = _unitsProxy.GetUnits(_playerControl.Value.Move.Round);

            float bestDist = 999;
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

            if(bestUnit != null && _currSpottedUnit != bestUnit)
            {
                _currSpottedUnit = bestUnit;
                OnUnitSpotted?.Invoke(bestUnit);
            }
        }
    }
}