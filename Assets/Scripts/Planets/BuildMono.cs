using System;
using System.Drawing;
using UnityEngine;


namespace Ulf
{

    public class BuildMono : MonoBehaviour
    {
        [SerializeField] private DefaultBuildStruct defaultBuild;

        public Vector2 size => GetComponent<BoxCollider2D>().size;
        public ElementType Element => defaultBuild.ElementType;
        public DefaultBuildStruct DefaultBuild => defaultBuild;

        void Start()
        {

        }

        internal void Init(Planet planet, CreateBuildStruct createBuildStruct)
        {
            Vector2 pos = CircleMove.GetMovePos(planet.Position, planet.Radius, createBuildStruct.Angle);

            transform.position = pos;
            gameObject.name = "Build " + createBuildStruct.Angle.ToString();

            float angle = planet.RoundMono.LookAtCenter(transform);
            transform.Rotate(0, 0, angle);
        }
    }
}