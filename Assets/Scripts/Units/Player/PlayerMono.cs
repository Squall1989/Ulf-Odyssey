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
            var health = new Health(createUnit.Health, planet.Element);

            _movement.Init(planet, freeArc);
            _action.Init(action);

            _movementMono = _movement as PlayerMovementMono;
            _player = new Player(createUnit, defaultUnit, _movement.CircleMove, action, health);
            _unit = _player;

            health.SetKillables(GetKillables());
        }

    }
}