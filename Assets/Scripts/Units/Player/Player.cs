using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ulf
{
    public class Player : Unit
    {
        private ExtendedCircleMove circleMoveExtended;
        public ExtendedCircleMove ExtendedCircleMove => circleMoveExtended;
        
        public Func<int, IRound> GetRoundFromId;

        public Player(ElementType elementType, CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, ExtendedCircleMove circleMove) : base(elementType, unitStruct, defaultUnit, circleMove)
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
    }
}
