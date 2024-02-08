
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ulf
{
    public class SceneBase
    {
        protected List<Planet> planets = new();
        protected List<IRound> rounds = new();
        protected List<Player> players = new();

        protected Player _player;

        protected Action<ActionData> OnPlayerAction;

        public void AddPlanet(Planet planet)
        {
            planets.Add(planet);
            rounds.Add(planet);
        }

        public void AddBridge(Bridge bridge)
        {
            rounds.Add(bridge);
        }

        public void AddPlayer(Player player, bool isOurPlayer)
        {
            if (isOurPlayer)
            {
                _player = player;
            }

            players.Add(player);
        }

        public void DoPlayerAction(ActionData playerActionData)
        {
            var player = players.First(p => p.GUID == playerActionData.guid);
            playerActionData.action.DoAction(player);
        }

        public void CreatePlayerStandAction(int id, float angle)
        {
            StandAction standAction = new StandAction()
            {
                degree = angle,
                roundId = id,
            };

            standAction.DoAction(_player);

            OnPlayerAction?.Invoke(new ActionData()
            {
                action = standAction,
                guid = _player.GUID,
                isPlayerAction = true
            });
        }

        public void CreatePlayerMoveAction(int direct)
        {
            MovementAction movementAction = new MovementAction()
            {
                direction = direct,
                fromAngle = _player.Movement.Degree,
            };

            movementAction.DoAction(_player);

            OnPlayerAction?.Invoke(new ActionData()
            {
                action = movementAction,
                guid = _player.GUID,
                isPlayerAction = true
            });
        }

        public IRound GetRoundFromId(int id)
        {
            return rounds.FirstOrDefault(p => p.ID == id);
        }
    }
}