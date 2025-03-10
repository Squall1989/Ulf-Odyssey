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
            var action = new ActionUnit();
            _action.Init(action);
            var circleMove = new ExtendedCircleMove();
            circleMove.OnLog += (log) => Debug.Log(log);
            _movement.Init(planet, circleMove, freeArc);

            _movementMono = _movement as PlayerMovementMono;
            _player = new Player(planet.Element, createUnit, defaultUnit, circleMove, action);
            _unit = _player;
        }


    }
}