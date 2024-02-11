using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Cinemachine;
using System;

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
        [Inject] protected IPlayerProxy playerProxy;
        [Inject] protected IUnitsProxy unitsProxy;
        [Inject] protected ConnectHandler connectHandler;
        [Inject] protected InputControl inputControl;
        [Inject] protected CinemachineVirtualCamera cameraControl;
        [Inject] protected PlayerMono playerPrefab;

        private List<PlanetMono> planetList = new();
        private List<BridgeMono> bridgeList = new();

        async void Start()
        {
            var scene = await sceneProxy.GetSceneStruct();
            InstPlanets(scene);
            playerProxy.OnOtherPlayerSpawn += (playerStruct) => InstPlayer(playerStruct, false);
            var player = await playerProxy.SpawnPlayer();
            InstPlayer(player, true);
            ConnectBridges();
        }

        private void ConnectBridges()
        {
            foreach (var bridge in bridgeList)
            {
                var planetId = bridge.CreateStruct.endPlanetId;
                var planetOut = GetPlanetFromId(planetId);
                Debug.Log($"endPlanetId: {planetId} planet: {planetOut.RoundMono.TransformRound}");
                bridge.OnSetBridge(planetOut);
            }
        }

        public Planet GetPlanetFromId(int id)
        {
            foreach(var planet in planetList)
            {
                if (planet.Planet.ID == id)
                    return planet.Planet;
            }

            return null;
        }

        private void InstPlayer(SnapPlayerStruct player, bool ourPlayer)
        {
            var planet = planetList.First(p => p.Planet.ID == player.planetId);
            var unitMono = planet.InstUnit(playerPrefab, player.snapUnitStruct, null);
            PlayerMono playerMono = unitMono as PlayerMono;
            playerMono.Player.GetRoundFromId = sceneProxy.GetRoundFromId;
            unitMono.transform.parent = null;
            unitMono.transform.localScale = new Vector3(2,2,2);
            unitMono.gameObject.name = "Player";
            playerProxy.AddPlayer(playerMono.Player, ourPlayer);
            
            if(ourPlayer)
                SetupControl(unitMono);

        }

        private void SetupControl(UnitMono unitMono)
        {
            var extendMovement = ((unitMono as PlayerMono).CircleMove as ExtendedCircleMove);
            inputControl.OnMove += (direct) =>
            {
                playerProxy.CreatePlayerMoveAction(direct);
            };
            inputControl.OnStand += (direct) =>
            {
                extendMovement.SetStandDirect(direct);
                //sceneProxy.CreatePlayerStandAction(direct);
            };

            cameraControl.Follow = unitMono.transform;
            cameraControl.LookAt = unitMono.transform;
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

                if(planetNew.BridgesMono != null)
                    bridgeList.AddRange(planetNew.BridgesMono);

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