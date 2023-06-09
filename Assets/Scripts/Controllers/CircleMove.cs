using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class CircleMove : IMovable
    {
        private float deltaTime;
        private float radius;
        private float speedLinear;
        private float currDegree;

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
        }

        public void ToLand(float radius, float startDegree)
        {
            this.radius = radius;
            currDegree = startDegree;
        }

        public (float x, float y) Move(int direct)
        {
            float speedRadial = direct * speedLinear * deltaTime / radius;
            float newDegree = (currDegree + speedRadial) / 180 * Mathf.PI;

            float planetPosX = radius * Mathf.Sin(newDegree);
            float planetPosY = radius * Mathf.Cos(newDegree);

            return (planetPosX, planetPosY);
        }
    }
}
