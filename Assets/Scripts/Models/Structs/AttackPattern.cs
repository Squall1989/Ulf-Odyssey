
[System.Serializable]
public struct AttackPattern
{
    public InitiativeType initiative;
    public AttackChoose[] attackChooses;
}

[System.Serializable]
public struct AttackChoose
{
    public ConditionType condition;
    public AttackType attackType;
}

public enum ConditionType
{
    none,
    lowHP,
    highHP,
    closeFight,
    distFight,
    bothFights,
    retreating,
}

public enum AttackType
{
    none,
    first,
    second,
    third,
}

public enum InitiativeType
{
    none,
    justAttack,
    firstNumber,
    secondNumber,
}