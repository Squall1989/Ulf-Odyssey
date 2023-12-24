using System;
using System.Drawing;
using UnityEngine;


namespace Ulf
{
    public class BuildMono : MonoBehaviour
    {
        [SerializeField] private DefaultBuildStruct defaultBuild;

        public ElementType Element => defaultBuild.ElementType;
        public DefaultBuildStruct DefaultBuild => defaultBuild;

        void Start()
        {

        }

        internal void Init(Planet planet, CreateBuildStruct createBuildStruct, float angle)
        {
            Vector2 pos = CircleMove.GetMovePos(planet.Position, planet.Radius, angle);

            transform.position = pos;

            MovementMono.LookAtPlanet(transform, planet.Position);

            pos = CircleMove.GetMovePos(planet.Position, planet.Radius, angle);

            transform.position = pos;
        }
    }
}