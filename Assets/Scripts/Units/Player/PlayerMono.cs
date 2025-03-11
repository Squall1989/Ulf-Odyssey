using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{
    public class PlayerMono : UnitMono
    {
        private Player _player;

        private PlayerMovementMono _movementMono;

        public Player Player => _player;
        public PlayerMovementMono PlayerMovement => _movementMono;

        public override void Init(Planet planet, CreateUnitStruct createUnit, float freeArc)
        {
            var action = new ActionUnit(createUnit.Guid);
            var circleMove = new ExtendedCircleMove();
            var health = new Health(createUnit.Health, planet.Element);

            _movementMono = _movement as PlayerMovementMono;
            _player = new Player(createUnit, defaultUnit, circleMove, action, health);
            _unit = _player;

            circleMove.OnLog += (log) => Debug.Log(log);

            _movement.Init(planet, circleMove, freeArc);
            _action.Init(action);
            health.SetKillables(GetKillables());
        }

    }
}