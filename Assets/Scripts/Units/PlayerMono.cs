using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{
    public class PlayerMono : UnitMono
    {
        private Player _player; 

        public Player Player => _player;

        protected override void CreateUnit(Planet planet, CreateUnitStruct createUnit)
        {
            _player = new Player(planet.Element, createUnit, defaultUnit, CircleMove);
            _unit = _player;
        }
    }
}