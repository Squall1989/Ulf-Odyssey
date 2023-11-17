namespace Ulf
{
    [System.Serializable]
    public struct SnapPlanetStruct
    {
        public CreatePlanetStruct createPlanet;
        public SnapUnitStruct[] snapUnits;
    }

    [System.Serializable]
    public struct SnapUnitStruct
    {
        public CreateUnitStruct createUnit;
        public float angle;
        public int health;
    }


    [System.Serializable]
    public struct SnapSceneStruct
    {
        public SnapPlanetStruct[] snapPlanets;
    }

}