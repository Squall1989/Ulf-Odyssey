
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ulf;
using System.Linq;

[CreateAssetMenu(fileName = "Units", menuName = "ScriptableObjects/Units", order = 2)]
public class AllUnitsScriptable : ScriptableObject
{
    [SerializeField] private UnitMono[] unitsMono;

    public IEnumerable<CreateUnitStruct> AllUnits => unitsMono.Select(p => p.UnitStruct);

    //public void AddUnit(CreateUnitStruct unitStruct)
    //{
    //    if(UnitsList.ContainsKey(unitStruct.View))
    //    {
    //        UnitsList[unitStruct.View] = unitStruct;
    //    }
    //    else
    //    {
    //        UnitsList.Add(unitStruct.View, unitStruct);
    //    }
    //    Debug.Log("Unit " + unitStruct.View + "saved. Units count: " + UnitsList.Count);
    //    EditorUtility.SetDirty(this);
    //}
}
