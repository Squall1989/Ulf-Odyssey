using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ulf
{
    /// <summary>
    /// Mono creator for single and muilty-player games
    /// </summary>
    public class GameCreator : MonoBehaviour
    {
        [Inject] protected AllPlanetsScriptable planetsContainer;
        [Inject] protected AllUnitsScriptable unitsContainer;
        [Inject] protected ISceneProxy sceneProxy;
        [Inject] protected IUnitsProxy unitsProxy;
        [Inject] protected ConnectHandler connectHandler;

        async void Start()
        {
            var scene = await sceneProxy.GetSceneStruct();
            InstPlanets(scene);
        }


        private void InstPlanets(List<SnapPlanetStruct> planetStructs)
        {
            foreach (var planetStruct in planetStructs)
            {
                var prefab = planetsContainer.GetPlanet(planetStruct.createPlanet);
                if (prefab == null)
                {
                    Debug.LogError($"Planet prefab with size {planetStruct.createPlanet.planetSize} and element {planetStruct.createPlanet.ElementType} is NULL!!!");
                    continue;
                }
                Debug.Log("Planet: " + prefab);
                var planetNew = Instantiate(prefab, planetStruct.createPlanet.planetPos, Quaternion.identity);
                planetNew.Init(planetStruct.createPlanet);
                var units = unitsContainer.GetUnits(planetStruct.createPlanet.createUnits);

                planetNew.InstUnits(units, planetStruct.snapUnits, unitsProxy);

                sceneProxy.AddPlanet(planetNew.Planet);
            }
        }
    }
}