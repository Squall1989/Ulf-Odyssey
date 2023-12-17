using System.Collections;
using System.Collections.Generic;
using Ulf;
using UnityEngine;

[CreateAssetMenu(fileName = "Builds", menuName = "ScriptableObjects/Builds", order = 4)]
public class AllBuildScriptable : ScriptableObject
{
    [SerializeField] private BuildMono[] builds;
}
