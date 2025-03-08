[System.Serializable]
public struct StatStruct 
{
    public StatType stat;
    public float amount;
}

public enum StatType
{
    none,
    lookDist,
    attackDist,
    walkSpeed,
    runSpeed,
    patrolDist,
}