using System;

namespace Ulf
{
    public class Player : Unit
    {
        private string _playerId;
        private ExtendedCircleMove circleMoveExtended;
        public ExtendedCircleMove ExtendedCircleMove => circleMoveExtended;

        public string PlayerID => _playerId;
        public Func<int, IRound> GetRoundFromId;

        public Player(ElementType elementType, CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, ExtendedCircleMove circleMove, ActionUnit action) : base(elementType, unitStruct, defaultUnit, circleMove, action)
        {
            circleMoveExtended = circleMove;
        }

        internal void StandTo(int roundId, float degree)
        {
            if (GetRoundFromId == null)
            {
                throw new Exception("GetRoundFromId is Null");
            }

            var round = GetRoundFromId.Invoke(roundId);

            circleMoveExtended.ToLand(round, degree, round is Bridge);
        }

        internal SnapPlayerStruct GetSnapshot()
        {
            var snapUnit = base.GetSnapshot();

            return new SnapPlayerStruct()
            {
                snapUnitStruct = snapUnit,
                planetId = circleMoveExtended.Round.ID,
                playerId = _playerId,
            };
        }

        internal void SetPlayerId(string playerId)
        {
            _playerId = playerId;
        }
    }
}
