
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ulf
{
    public class UnitsController : IUnitsProxy
    {
        private INetworkable _networkable;
        private List<Unit> _units = new();

        public UnitsController(INetworkable networkable) 
        {
            _networkable = networkable;
            _networkable.RegisterHandler<ActionData>(UnitNetworkAction);
        }

        private void UnitNetworkAction(ActionData action)
        {
            var unit = _units.FirstOrDefault(p => p.GUID == action.guid);
            if (unit == null) 
            {
                //throw new Exception("Unit is NULL!!!");
                return;
            }
            
            action.action.DoAction(unit);
        }

        public void Add(Unit unit)
        {
            _units.Add(unit);
        }

        public List<Unit> GetUnits(IRound round)
        {
            return _units.Where(p => p.Move.Round ==  round).ToList();
        }

        public List<Unit> GetUnits()
        {
            return _units;
        }
    }
}