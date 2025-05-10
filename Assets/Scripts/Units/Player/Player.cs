using System;

namespace Ulf
{
    public class Player : Unit
    {
        private string _playerId;
        public ExtendedCircleMove ExtendedCircleMove => Move as ExtendedCircleMove;

        public string PlayerID => _playerId;
        public Func<int, IRound> GetRoundFromId;

        public Player(CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, CircleMove circleMove, ActionUnit action, Health health) 
            : base(unitStruct, defaultUnit, circleMove, action, health)
        {
        }

        internal void StandTo(int roundId, float degree)
        {
            if (GetRoundFromId == null)
            {
                throw new Exception("GetRoundFromId is Null");
            }

            var round = GetRoundFromId.Invoke(roundId);

            ExtendedCircleMove.ToLand(round, degree, round is Bridge);
        }

        internal SnapPlayerStruct GetSnapshot()
        {
            var snapUnit = base.GetSnapshot();

            return new SnapPlayerStruct()
            {
                snapUnitStruct = snapUnit,
                planetId = ExtendedCircleMove.Round.ID,
                playerId = _playerId,
            };
        }

        internal void SetPlayerId(string playerId)
        {
            _playerId = playerId;
        }
    }
}
