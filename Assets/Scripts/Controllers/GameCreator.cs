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
        [Inject] protected IPlayersProxy playerProxy;
        [Inject] protected IUnitsProxy unitsProxy;
        [Inject] protected ConnectHandler connectHandler;
        [Inject] protected InputControl inputControl;
        [Inject] protected CinemachineVirtualCamera cameraControl;
        [Inject] protected PlayerMono playerPrefab;
        [Inject] protected GameState gameState;

        private List<PlanetMono> planetList = new();
        private List<BridgeMono> bridgeList = new();

        async void Start()
        {
            gameState.Condition = GameCondition.preparing;

            var scene = await sceneProxy.GetSceneStruct();
            InstPlanets(scene);
            playerProxy.OnOtherPlayerSpawn += (playerStruct) => InstPlayer(playerStruct, false);
            var player = await playerProxy.SpawnPlayer();
            InstPlayer(player, true);
            ConnectBridges();

            gameState.Condition = GameCondition.allIsReady;
        }

        private void ConnectBridges()
        {
            foreach (var bridge in bridgeList)
            {
                var planetId = bridge.CreateStruct.endPlanetId;
                var planetOut = GetPlanetFromId(planetId);
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
            playerMono.Player.SetPlayerId(player.playerId);
            playerProxy.AddPlayer(playerMono.Player, ourPlayer);

            if (ourPlayer)
            {
                SetupControl(unitMono);
                var container = ProjectContext.Instance.Container;
                container.Bind<Player>().FromInstance(playerMono.Player).AsSingle();
            }

        }

        private void SetupControl(UnitMono unitMono)
        {
            var playerMono = unitMono as PlayerMono;
            var extendMovement = playerMono.CircleMove as ExtendedCircleMove;
            inputControl.OnMove += (direct) =>
            {
                playerProxy.CreatePlayerMoveAction(direct);
            };

            inputControl.OnAttack += (num) =>
            {
                playerProxy.CreateUniversalAction(ActionType.attack, num);
                playerProxy.CreatePlayerMoveAction(0);
            };

            inputControl.OnStand += playerMono.PlayerMovement.ControlStandDirect;

            extendMovement.OnRoundStand += playerProxy.CreatePlayerStandAction;


            cameraControl.Follow = unitMono.transform;
            cameraControl.LookAt = unitMono.transform;
        }

        private void InstPlanets(List<SnapPlanetStruct> planetStructs)
        {
            foreach (var planetStruct in planetStructs)
            {
                var prefab = planetsContainer.GetPlanet(planetStruct.createPlanet.planetSize, planetStruct.createPlanet.ElementType);
                if (prefab == null)
                {
                    Debug.LogError($"Planet prefab with size {planetStruct.createPlanet.planetSize} and element {planetStruct.createPlanet.ElementType} is NULL!!!");
                    continue;
                }

                var planetNew = Instantiate(prefab, planetStruct.createPlanet.planetPos, Quaternion.identity);
                planetNew.Init(planetStruct.createPlanet);
                planetList.Add(planetNew);

                if (planetNew.BridgesMono != null)
                {
                    bridgeList.AddRange(planetNew.BridgesMono);
                }
                var units = unitsContainer.GetUnits(planetStruct.createPlanet.createUnits);

                var unitMonoList = planetNew.InstUnits(units, planetStruct.snapUnits, unitsProxy);

                var buildStructs = planetStruct.createPlanet.builds;

                if (buildStructs != null && buildStructs.Length > 0)
                {
                    planetNew.InstBuilds(buildContainer.GetBuilds(buildStructs), unitMonoList, buildStructs);
                }
                sceneProxy.AddPlanet(planetNew.Planet);
            }


            foreach (var bridgeMono in bridgeList)
            {
                sceneProxy.AddBridge(bridgeMono.Bridge);
            }
        }
    }
}