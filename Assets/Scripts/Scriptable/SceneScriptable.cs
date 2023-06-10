using System.Collections.Generic;
using Ulf;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObjects/Scene", order = 1)]
public class SceneScriptable : ScriptableObject
{
    public List<PlanetMono> planets;

}
