using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Planet : IRound
    {
        private IRoundMono _roundMono;
        private CreatePlanetStruct _planetStruct;
        private List<Unit> units;

        public ElementType Element => _planetStruct.ElementType;
        public float Radius => _planetStruct.planetSize;
        public Vector2 Position => _planetStruct.planetPos;
        public int ID => _planetStruct.planetId;
        public IRoundMono RoundMono => _roundMono;

        public Planet(CreatePlanetStruct planetStruct, IRoundMono roundMono)
        {
            _roundMono = roundMono;
            _planetStruct = planetStruct;
            units = new List<Unit>(planetStruct.createUnits.Length);
        }

        public void AddUnit(Unit unit) 
        {
            units.Add(unit);
        }


        public void RmUnit(Unit unit)
        {
            units.Remove(unit);
        }

        internal SnapPlanetStruct GetSnapshot()
        {
            return new SnapPlanetStruct()
            {
                createPlanet = _planetStruct,
                snapUnits = units.Select(u => u.GetSnapshot()).ToArray(),
                
            };
        }

        /*
        public IEnumerable<IMovable> GetAttackables(AttackableType attackableType, Vector2 pos, float interestDist)
        {

            return units.Where(unit => unit.Attackable.AttackableType == attackableType
                    && (unit.Movable.Position - pos).magnitude <= interestDist)
                    .Select(unit => unit.Movable);
        }
        */
    }
}