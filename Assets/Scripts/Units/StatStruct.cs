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
    _free_slot_,
    walkSpeed,
    runSpeed,
    patrolDist,
}