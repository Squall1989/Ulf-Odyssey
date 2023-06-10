using Ulf;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor helping script
/// When planet added, put it down to scriptable
/// </summary>
public class SceneHub : MonoBehaviour
{
    [SerializeField] SceneScriptable sceneScriptable;

    public void UpdateScene(PlanetMono planet)
    {
        if(!sceneScriptable.planets.Contains(planet))
            sceneScriptable.planets.Add(planet);
        EditorUtility.SetDirty(sceneScriptable);
    }
}
