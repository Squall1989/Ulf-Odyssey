using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ulf
{
    public abstract class PlayerControlBase
    {
        protected List<Player> players = new();
        protected Player _player;


        public Action<SnapPlayerStruct> OnOtherPlayerSpawn { get; set; }


        public void AddPlayer(Player player, bool isOurPlayer)
        {
            if (isOurPlayer)
            {
                _player = player;
            }

            players.Add(player);
        }

        public List<SnapPlayerStruct> PlayersSnapshot()
        {
            return players.Select(p => p.GetSnapshot()).ToList();
        }

        protected virtual void OurPlayerAction(ActionData playerActionData)
        {
            DoPlayerAction(playerActionData);
        }

        protected void DoPlayerAction(ActionData playerActionData)
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

            OurPlayerAction(new ActionData()
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

            OurPlayerAction(new ActionData()
            {
                action = movementAction,
                guid = _player.GUID,
                isPlayerAction = true
            });
        }
    }
}