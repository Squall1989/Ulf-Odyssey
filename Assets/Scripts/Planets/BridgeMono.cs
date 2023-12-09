using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ulf
{
    public class BridgeMono : MonoBehaviour
    {
        [SerializeField]
        private CircleCollider2D circleCollider;

        public float Size => circleCollider.radius * 2f;

        public void Init(Planet planet, float angle)
        {
            Vector2 pos = CircleMove.GetMovePos(planet.Position, planet.Radius + Size / 2f, angle);

            transform.position = pos;

            MovementMono.LookAtPlanet(transform, planet.Position);

            pos = CircleMove.GetMovePos(planet.Position, planet.Radius + Size / 2f, angle);

            transform.position = pos;
        }
    }
}