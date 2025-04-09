using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Zenject;

namespace Ulf
{
    /// <summary>
    /// To control world local events
    /// </summary>
    public class WorldView 
    {
        public Action<float2, float2, ElementType> OnUnitDamaged;
        private IUnitsProxy _unitsProxy;
        private IPlayersProxy _playersProxy;

        [Inject]
        public WorldView(IUnitsProxy unitsProxy, IPlayersProxy playersProxy)
        {
            _unitsProxy = unitsProxy;
            _playersProxy = playersProxy;

        }

        public void Init()
        {
            InitDamages(_unitsProxy.GetUnits());
            InitDamages(_playersProxy.PlayersList.Select(p => p as Unit));
        }

        private void InitDamages(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                unit.Actions.OnAttacked += UnitDamaged;
            }
        }

        private Unit FindUnit(int id)
        {
            foreach (var unit in _unitsProxy.GetUnits())
            {
                if(id == unit.GUID) 
                    return unit;
            }

            foreach (var player in _playersProxy.PlayersList)
            {
                if (id == player.GUID) 
                    return player;
            }

            throw new Exception($"Unit with id {id} not found!");
        }

        private void UnitDamaged((int damage, int attackable, int attacker) DamageParams)
        {
            Unit damager = FindUnit(DamageParams.attacker);
            Unit attackable = FindUnit(DamageParams.attackable);

            float2 pos = attackable.Move.Position;
            float2 dir = pos + pos - (float2)damager.Move.Position;
            ElementType element = attackable.Health.Element;

            OnUnitDamaged?.Invoke(pos, dir, element);
        }
    }
}