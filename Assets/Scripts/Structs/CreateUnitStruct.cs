using System.Collections.Generic;

[System.Serializable]
public struct CreateUnitStruct 
{
    public string View;
    public ElementType ElementType;
    public int Health;
    public float MoveSpeed;
}
[System.Serializable]
public struct CreatePlanetStruct
{
    public const int maxSize = 4;

    public int planetId;

    public CreateUnitStruct[] createUnits;
    public ElementType ElementType;
    public int planetSize;
}
[System.Serializable]
public struct BridgePositionStruct
{
    public float angleStart;
    public bool mirrorLeft;
    public int startPlanetId;
    public int endPlanetId;
}

[System.Serializable]
public struct CreateSceneStruct
{
    public CreatePlanetStruct[] planets;
}