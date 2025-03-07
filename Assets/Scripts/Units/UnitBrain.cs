using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ulf
{
    /// <summary>
    /// Supporting class for units decisions
    /// </summary>
    public class UnitsBrain
    {
        private IPlayersProxy _playersProxy;
        private StatsScriptable[] _stats;
        private Unit _unit;
        private Player _player;

        public UnitsBrain(IPlayersProxy playersProxy, StatsScriptable[] stats)
        {
            _playersProxy = playersProxy;
            _stats = stats;
        }

        public void InitNextUnit(Unit unit)
        {
            _unit = unit;
            IRound planet = unit.Move.Round;
            float playerDist = 999;
            Player closestPlayer = null;
            foreach (var player in _playersProxy.PlayersList)
            {
                if (player.Move.Round == planet)
                {
                    float dist = math.distance(unit.Move.Position, player.Move.Position);
                    if(dist < playerDist)
                    {
                        playerDist = dist;
                        closestPlayer = player;
                    }
                }
            }

            if(closestPlayer != null)
            {
                _player = closestPlayer;
            }
        }

        public MoveType DecideMove(Unit unit, float lookDist)
        {
            if (unit != _unit)
            {
                throw new Exception("Unit not initialized!");
            }

            if (_player != null)
            {
                float playerDist = (_player.Move.Position - _unit.Move.Position).magnitude;

                if (playerDist > lookDist)
                {
                    return MoveType.walk;
                }

                Vector2 playerPlanetVector = _player.Move.Position - _player.Move.PlanetPosition;
                Vector2 unitPlanetVector = _unit.Move.Position - _unit.Move.PlanetPosition;
                bool playerRight = MathUtils.IsRightDir(unitPlanetVector, playerPlanetVector);

                bool lookToPlayer = playerRight == (_unit.Move.Direct == -1);
                if(lookToPlayer)
                {
                    return MoveType.run;
                }
            }

            return MoveType.walk;
        }

        public void DecideAction(Unit unit)
        {
            
        }
    }


}