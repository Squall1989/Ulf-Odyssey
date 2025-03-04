using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 6)]
public class StatsScriptable : ScriptableObject
{
    [SerializeField] private string unitId;
    [SerializeField] private StatStruct[] statStructs;

    public StatStruct[] StatStructs => statStructs;
    public string ID => unitId;

    public float GetStatAmount(StatType statType)
    {
        for (int i = 0; i < statStructs.Length; i++)
        {
            if (statStructs[i].stat == statType)
            {
                return statStructs[i].amount;
            }
        }

        return 0;

    }
}
