using MessagePack;

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

    [MessagePackFormatter(typeof(SnapUnitStruct))]
    public struct SnapUnitStruct
    {

        public CreateUnitStruct createUnit;
        public float angle;
        public int health;
    }



}