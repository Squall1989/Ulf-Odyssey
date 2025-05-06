using ENet;
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
        private readonly BehaviourScriptable _behaviour;
        private readonly IPlayersProxy _playersProxy;
        private readonly Unit _unit;
        private readonly StatsScriptable _unitStats;
        private readonly Vector2 _startPos;

        private float moveDecisionCooldown;

        public Action<Unit, int, float> OnUnitMove;
        public Action<Unit, ActionType, int> OnUnitAction;

        public UnitBrain(IPlayersProxy playersProxy, Unit unit, 
            StatsScriptable unitStats, BehaviourScriptable behaviour)
        {
            _behaviour = behaviour;
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
                if (player.Health.CurrHealth <= 0)
                {
                    continue;
                }
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
                bool isUnitLookToStart = UnitUtils.IsUnitLookTo(_unit, _startPos);

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

        private bool DecidesAttack(Player treatPlayer, out int attackNum)
        {
            attackNum = -1;

            if (!LookAtPlayer(treatPlayer, out float dist))
            {
                return false;
            }

            for(int i = 0; i < _behaviour.attackPatterns.Length; i++)
            {
                var pattern = _behaviour.attackPatterns[i];
                bool isAttackAllow = true;

                if (pattern.attackDist < dist)
                {
                    continue;
                }

                for (int j = 0; j < pattern.conditions.Length; j++)
                {
                    if (!SwitchConditions(pattern.conditions[j], dist))
                    {
                        isAttackAllow = false;
                        break;
                    }
                }

                if (isAttackAllow)
                {
                    attackNum = _behaviour.attackPatterns[i].attackNum;
                    return true;
                }
            }

            return false;
        }

        private bool SwitchConditions(ConditionType condition, float dist)
        {
            float runSpeed = _unitStats.GetStatAmount(StatType.runSpeed);
            switch (condition)
            {
                case ConditionType.notRunning:
                    return _unit.Move.Speed < runSpeed;
                case ConditionType.afterRun:
                    return _unit.Move.Speed == runSpeed;
                case ConditionType.afterAttack:
                    return _unit.Actions.Attacker > -1;
                case ConditionType.closeFight:
                    return dist < 5;
                case ConditionType.none:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool LookAtPlayer(Player treatPlayer, out float playerDist)
        {
            playerDist = (treatPlayer.Move.Position - _unit.Move.Position).magnitude;
            float lookDist = _unitStats.GetStatAmount(StatType.lookDist);

            if (playerDist > lookDist)
            {
                return false;
            }

            return UnitUtils.IsUnitLookTo(_unit, treatPlayer.Move.Position);
        }

        private bool DecidesRun(Player treatPlayer, out int direct)
        {
            direct = 0;
            if (treatPlayer != null)
            {

                float runSpeed = _unitStats.GetStatAmount(StatType.runSpeed);

                if (_unit.Move.Speed == runSpeed)
                {
                    return false;
                }

                bool lookToPlayer = LookAtPlayer(treatPlayer, out var dist);

                if (lookToPlayer)
                {
                    direct = _unit.Move.Direct;
                    return true;
                }
                else if (_unit.Actions.Attacker > -1)
                {
                    direct = -_unit.Move.Direct;
                    return true;
                }
            }

            return false;
        }

        private float CalcAimAngle(Player treatPlayer)
        {
            Vector2 unitPlanetVect = _unit.Move.PlanetPosition - _unit.Move.Position;
            Vector2 playerPlanetVect = treatPlayer.Move.PlanetPosition - treatPlayer.Move.Position;

            if (unitPlanetVect.magnitude > playerPlanetVect.magnitude + 1f) // Stand on the Tower
            {
                return Vector2.Angle(unitPlanetVect, playerPlanetVect);
            }
            else
            {
                return 0;
            }
        }

        public void MakeDecision(float deltaTime)
        {
            moveDecisionCooldown -= deltaTime;
            if (_unit.Actions.IsActionInProcess
                || _unit.Health.CurrHealth <= 0)
            {
                return;
            }

            if(FindPlayer(out var player))
            {
                if (DecidesAttack(player, out int attackNum))
                {
                    if (_unitStats.GetStatAmount(StatType.shootAngle) > 0)
                    {
                        OnUnitAction?.Invoke(_unit, ActionType.aim, (int)CalcAimAngle(player));
                    }

                    OnUnitAction?.Invoke(_unit, ActionType.attack, attackNum +1);

                    if (_unit.Move.Speed > 0)
                    {
                        OnUnitMove?.Invoke(_unit, 0, 0);
                    }
                    return;
                }
                else if (DecidesRun(player, out int direct))
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
                    speed = _unitStats.GetStatAmount(StatType.walkSpeed);
                }
                OnUnitMove?.Invoke(_unit, dir, speed);
                moveDecisionCooldown = Random.Range(1, 4);
            }
        }
    }


}