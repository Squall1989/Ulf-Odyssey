using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Ulf
{
    /// <summary>
    /// To control world local events
    /// </summary>
    public class WorldView 
    {
        public Action<(int damage, int attackable, int attacker)> OnUnitDamaged;

        [Inject]
        public WorldView(IUnitsProxy unitsProxy, IPlayersProxy playersProxy)
        {

            InitDamages(unitsProxy.GetUnits());
            InitDamages(playersProxy.PlayersList.Select(p => p as Unit));
        }

        private void InitDamages(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                unit.Actions.OnAttacked += UnitDamaged;
            }
        }

        private void UnitDamaged((int damage, int attackable, int attacker) DamageParams)
        {
            OnUnitDamaged?.Invoke(DamageParams);
        }
    }
}