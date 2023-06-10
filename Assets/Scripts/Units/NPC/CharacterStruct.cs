using System;

[Serializable]
public struct CharacterStruct 
{
    public AgrType startAgr;
    public float interactDist;
}

public enum AgrType
{
    neutral = 0,
    agressive = 1,
    peace = 2,
}
