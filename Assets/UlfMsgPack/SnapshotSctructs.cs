using MessagePack;
using MsgPck;

namespace Ulf
{
    [MessagePackObject]
    public struct SnapPlanetStruct
    {
        [Key(0)]
        public CreatePlanetStruct createPlanet;
        [Key(1)]
        public SnapUnitStruct[] snapUnits;
    }

    [MessagePackObject]
    public struct SnapUnitStruct
    {

        [Key(0)]
        public CreateUnitStruct createUnit;
        [Key(1)]
        public float angle;
        [Key(2)]
        public int health;
    }

    [MessagePackObject]
    public struct SnapPlayerStruct : IUnionMsg
    {
        [Key(0)]
        public SnapUnitStruct snapUnitStruct;
        [Key(1)]
        public int planetId;
        [Key(2)]
        public string playerId;
    }

}