using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ulf
{
    public class Player : Unit
    {
        private ExtendedCircleMove circleMoveExtended;
        public ExtendedCircleMove ExtendedCircleMove => circleMoveExtended;
        
        public Player(ElementType elementType, CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, CircleMove circleMove) : base(elementType, unitStruct, defaultUnit, circleMove)
        {
            circleMoveExtended = new ExtendedCircleMove(defaultUnit.MoveSpeed);
        }

    }
}
