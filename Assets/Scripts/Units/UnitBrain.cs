using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ulf
{
    /// <summary>
    /// Supporting class for units decisions
    /// </summary>
    public class UnitBrain
    {
        private readonly IPlayersProxy _playersProxy;
        private readonly Unit _unit;
        private readonly StatsScriptable _unitStats;
        private readonly Vector2 _startPos;

        private float moveDecisionCooldown;

        public Action<Unit, int, float> OnUnitMove;
        public Action<Unit, ActionType, int> OnUnitAction;

        public UnitBrain(IPlayersProxy playersProxy, Unit unit, StatsScriptable unitStats)
        {
            _playersProxy = playersProxy;
            _unit = unit;
            _unitStats = unitStats;
            _startPos = unit.Move.Position;
        }

        private bool FindPlayer(out Player closestPlayer)
        {
            IRound planet = _unit.Move.Round;
            float playerDist = 999;
            closestPlayer = null;
            foreach (var player in _playersProxy.PlayersList)
            {
                if (player.Move.Round == planet)
                {
                    float dist = math.distance(_unit.Move.Position, player.Move.Position);
                    if(dist < playerDist)
                    {
                        playerDist = dist;
                        closestPlayer = player;
                    }
                }
            }

            return closestPlayer != null;

        }

        private int DecidesWalk()
        {
            float patrolDist = _unitStats.GetStatAmount(StatType.patrolDist);

            float unitAwayDist = (_startPos - _unit.Move.Position).magnitude;

            if(_unit.Move.Direct != 0 && unitAwayDist >= patrolDist)
            {
                bool isUnitLookToStart = IsUnitLookTo(_startPos);

                if(isUnitLookToStart)
                {
                    return _unit.Move.Direct;
                }
                else
                {
                    return _unit.Move.Direct * -1;
                }
            }
            else
            {
                return Random.Range(-1, 2);
            }
        }

        private bool IsUnitLookTo(Vector2 point)
        {
            Vector2 pointPlanetVector = point - _unit.Move.PlanetPosition;
            Vector2 unitPlanetVector = _unit.Move.Position - _unit.Move.PlanetPosition;
            bool pointRight = MathUtils.IsRightDir(unitPlanetVector, pointPlanetVector);

            bool lookToPoint = pointRight == (_unit.Move.Direct == -1);
            return lookToPoint;
        }

        private bool DecidesRun(Player treatPlayer, out int direct)
        {
            direct = 0;
            if (treatPlayer != null)
            {
                float playerDist = (treatPlayer.Move.Position - _unit.Move.Position).magnitude;
                float lookDist = _unitStats.GetStatAmount(StatType.lookDist);

                if (playerDist > lookDist)
                {
                    return false;
                }

                bool lookToPlayer = IsUnitLookTo(treatPlayer.Move.Position);

                if(lookToPlayer)
                {
                    direct = _unit.Move.Direct;
                    return true;
                }
            }

            return false;
        }

        public void MakeDecision(float deltaTime)
        {
            moveDecisionCooldown -= deltaTime;
            if (_unit.Actions.IsActionInProcess)
            {
                return;
            }

            if(FindPlayer(out var player))
            {
                if (DecidesRun(player, out int direct))
                {
                    float speed = _unitStats.GetStatAmount(StatType.runSpeed);
                    OnUnitMove?.Invoke(_unit, direct, speed);
                    return;
                }
            }

            if (moveDecisionCooldown <= 0 )
            {
                int dir = DecidesWalk();
                float speed = 0;
                if (dir != 0)
                {
                    _unitStats.GetStatAmount(StatType.walkSpeed);
                }
                OnUnitMove?.Invoke(_unit, dir, speed);
                moveDecisionCooldown = Random.Range(1, 4);
            }
        }
    }


}