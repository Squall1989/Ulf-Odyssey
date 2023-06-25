using Ulf;
using UnityEditor;
using UnityEngine;
using Zenject;

/// <summary>
/// Editor helping script
/// When planet added, put it down to scriptable
/// </summary>
public class SceneHub : MonoBehaviour
{
    [SerializeField] SceneScriptable sceneScriptable;
    [Inject] IGame game;

    public static SceneHub Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScene(PlanetMono planet)
    {
        if(!sceneScriptable.planets.Contains(planet))
            sceneScriptable.planets.Add(planet);
        EditorUtility.SetDirty(sceneScriptable);
    }

    public void InstantiateScene()
    {

    }
}
