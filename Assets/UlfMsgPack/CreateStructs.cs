using MessagePack;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace Ulf
{
    [MessagePackObject]
    public struct CreateUnitStruct
    {
        [Key(0)] 
        public string View;
        [Key(1)]
        public ElementType ElementType;
        [Key(2)] 
        public int Health;
        [Key(3)] 
        public float MoveSpeed;
    }
    [MessagePackObject]
    public struct CreatePlanetStruct
    {
        [Key(0)]
        public int planetId;

        [Key(1)]
        public CreateUnitStruct[] createUnits;
        [Key(2)]
        public ElementType ElementType;
        [Key(3)] 
        public float planetSize;
        [Key(4)] 
        public Vector3 planetPos;
    }
    [MessagePackObject]
    public struct BridgePositionStruct
    {
        [Key(0)]
        public float angleStart;
        [Key(1)]
        public bool mirrorLeft;
        [Key(2)]
        public int startPlanetId;
        [Key(3)]
        public int endPlanetId;
    }

    [MessagePackObject]
    public struct CreateSceneStruct
    {
        [Key(0)]
        public CreatePlanetStruct[] planets;
    }
}