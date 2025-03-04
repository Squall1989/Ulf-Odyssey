using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 6)]
public class StatsScriptable : ScriptableObject
{
    [SerializeField] private string unitId;
    [SerializeField] private StatStruct[] statStructs;

    public StatStruct[] StatStructs => statStructs;
    public string ID => unitId;
}
