[System.Serializable]
public struct CreateUnitStruct 
{
    public int Health;
    public float MoveSpeed;
}
[System.Serializable]
public struct CreatePlanetStruct
{
    public CreateUnitStruct[] createUnits;
    public ElementType ElementType;
    
}