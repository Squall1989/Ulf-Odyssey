using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] protected AllBuildScriptable buildContainer;
        [Inject] protected AllUnitsScriptable unitsContainer;
        [Inject] protected ISceneProxy sceneProxy;
        [Inject] protected IUnitsProxy unitsProxy;
        [Inject] protected ConnectHandler connectHandler;
        [Inject] protected InputControl inputControl;

        private List<PlanetMono> planetList = new();

        async void Start()
        {
            var scene = await sceneProxy.GetSceneStruct();
            InstPlanets(scene);
            var player = await sceneProxy.SpawnPlayer();
            InstPlayer(player);
        }

        private void InstPlayer(SnapPlayerStruct player)
        {
            var planet = planetList.First(p => p.Planet.ID == player.planetId);
            var unitMonoDefault = unitsContainer.GetUnits( new CreateUnitStruct[] { player.snapUnitStruct.createUnit }).First();
            var unitMono = planet.InstUnit(unitMonoDefault, player.snapUnitStruct, unitsProxy);
            SetupControl(unitMono);
        }

        private void SetupControl(UnitMono unitMono)
        {

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
                planetList.Add(planetNew);
                var units = unitsContainer.GetUnits(planetStruct.createPlanet.createUnits);

                planetNew.InstUnits(units, planetStruct.snapUnits, unitsProxy);

                var buildStructs = planetStruct.createPlanet.builds;
                if (planetStruct.createPlanet.builds.Length > 0)
                {
                    planetNew.InstBuilds(buildContainer.GetBuilds(buildStructs), buildStructs);
                }
                sceneProxy.AddPlanet(planetNew.Planet);
            }
        }
    }
}