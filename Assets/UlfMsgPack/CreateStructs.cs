using MessagePack;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    [System.Serializable]
    public struct DefaultUnitStruct
    {
        public string View;
        public ElementType ElementType;
        public int Health;
        public float MoveSpeed;
    }

    [MessagePackObject]
    public struct CreateUnitStruct
    {
        [Key(0)] 
        public string View;
        [Key(1)]
        public int Guid;
        [Key(2)]
        public int Health;
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
        public Vector2 planetPos;
        [Key(5)]
        public CreateBridgeStruct[] bridges;
    }
    [MessagePackObject]
    public struct CreateBridgeStruct
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