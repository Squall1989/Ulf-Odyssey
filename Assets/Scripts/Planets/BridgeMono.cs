using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{
    public class BridgeMono : MonoBehaviour, IRoundMono
    {
        [SerializeField]
        private CircleCollider2D circleCollider;
        private CreateBridgeStruct _createBridgeStruct;
        private Bridge _bridge;
        public float Size => circleCollider.radius * 2f;
        public CreateBridgeStruct CreateStruct => _createBridgeStruct;
        public Action<Planet> OnSetBridge => _bridge.ConnectOutPlanet;
                public Transform TransformRound => transform;

        public Bridge Bridge => _bridge;

        public void Init(Planet planet, CreateBridgeStruct createBridgeStruct)
        {
            Vector2 pos = CircleMove.GetMovePos(planet.Position, planet.Radius + Size / 2f, createBridgeStruct.angleStart);
            transform.position = pos;

            gameObject.name = "Bridge " + createBridgeStruct.angleStart.ToString();

            planet.RoundMono.LookAtCenter(transform);
            if (createBridgeStruct.mirrorLeft)
            {
                transform.Rotate(Vector3.up, 180f);
            }

            _createBridgeStruct = createBridgeStruct;
            Physics.SyncTransforms();
            _bridge = new Bridge(!createBridgeStruct.mirrorLeft, circleCollider.radius, transform.position, planet, this);
        }

        public void LookAtCenter(Transform playerTransform)
        {
            Vector3 relative = playerTransform.InverseTransformPoint(transform.position);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            playerTransform.Rotate(0, 0, -angle);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Enter " + collision.gameObject.name);
            if(collision.TryGetComponent<PlayerMono>(out var playerMono))
            {
                playerMono.Player.ExtendedCircleMove.SetBridge(_bridge);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Exit");


            if (collision.TryGetComponent<PlayerMono>(out var playerMono))
            {
                playerMono.Player.ExtendedCircleMove.SetBridge(null);
            }
        }
    }
}