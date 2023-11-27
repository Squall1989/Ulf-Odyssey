
using System.Collections.Generic;
using Zenject;
using Time = UnityEngine.Time;
using Random = UnityEngine.Random;

namespace Ulf
{
    public class UnitsBehaviour : IUnitsProxy, ITickable
    {
        protected List<Unit> units = new();
        protected Dictionary<Unit, BehaviourUnitStruct> unitsBehaviourDict = new();
        private INetworkable _networkable;

        public UnitsBehaviour(INetworkable networkable) 
        {
            _networkable = networkable;
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
                unitsBehaviourDict[unit].timer.RemainingTime -= Time.time;
            }
        }

        private BehaviourUnitStruct GetNextAction(Unit unit)
        {
            int direct = Random.Range(-1, 1);
            int time = Random.Range(3, 10);

            return new BehaviourUnitStruct()
            {
                nextAction = new MovementAction() { direction = direct },
                timer = new Timer(time),
            };
        }
    }


}