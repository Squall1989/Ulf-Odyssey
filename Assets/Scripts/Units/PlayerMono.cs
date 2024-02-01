using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{
    public class PlayerMono : UnitMono
    {
        private Player _player;

        public Player Player => _player;

        public override void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            var circleMove = new ExtendedCircleMove(defaultUnit.MoveSpeed);
            movement.Init(planet, circleMove, freeArc);

            _player = new Player(planet.Element, createUnit, defaultUnit, circleMove);
            _unit = _player;
        }


    }
}