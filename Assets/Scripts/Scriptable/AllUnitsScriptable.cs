
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Units", menuName = "ScriptableObjects/Units", order = 2)]
public class AllUnitsScriptable : ScriptableObject
{
    Dictionary<string, CreateUnitStruct> UnitsList = new();

    public IEnumerable<CreateUnitStruct> AllUnits => UnitsList.Values;

    public void AddUnit(CreateUnitStruct unitStruct)
    {
        if(UnitsList.ContainsKey(unitStruct.View))
        {
            UnitsList[unitStruct.View] = unitStruct;
        }
        else
        {
            UnitsList.Add(unitStruct.View, unitStruct);
        }
        Debug.Log("Unit " + unitStruct.View + "saved. Units count: " + UnitsList.Count);
        EditorUtility.SetDirty(this);
    }
}
