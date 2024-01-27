using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ulf
{
    public class Player : Unit
    {
        public Player(ElementType elementType, CreateUnitStruct unitStruct, DefaultUnitStruct defaultUnit, CircleMove circleMove) : base(elementType, unitStruct, defaultUnit, circleMove)
        {
        }

    }
}
