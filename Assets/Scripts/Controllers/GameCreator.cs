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
        [Inject] protected SceneGenerator sceneGenerator;

        void Start()
        {
            InstPlanets(sceneGenerator.PlanetList);
        }


        private void InstPlanets(List<CreatePlanetStruct> planetStructs)
        {
            foreach(var planetStruct in planetStructs)
            {
                var prefab = planetsContainer.GetPlanet(planetStruct);
                if (prefab == null) 
                {
                    Debug.LogError($"Planet prefab with size {planetStruct.planetSize} and element {planetStruct.ElementType} is NULL!!!");
                    continue;
                }

                var planetNew = Instantiate(prefab, planetStruct.planetPos, Quaternion.identity);

                var units = unitsContainer.GetUnits(planetStruct.createUnits);
                planetNew.InstUnits(units);
            }
        }
    }
}