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
        [SerializeField] protected PlanetMono planetMono;
        [Inject] protected SceneGenerator sceneGenerator;

        void Start()
        {
            InstPlanets(sceneGenerator.PlanetList);
        }


        private void InstPlanets(List<CreatePlanetStruct> planetStructs)
        {
            foreach(var planetStruct in planetStructs)
            {
                Planet planet = new Planet(planetStruct.planetSize, planetStruct.ElementType);

                var planetNew = Instantiate(planetMono, planetStruct.planetPos, Quaternion.identity);
            }
        }
    }
}