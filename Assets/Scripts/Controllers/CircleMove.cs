using Assets.Scripts.Interfaces;
using System;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class CircleMove : IMovable
    {
        private float deltaTime;
        private float radius;
        private float speedLinear;
        private float currDegree;

        private Vector2 position;

        public float Degree => currDegree;

        public Vector2 Position => position;

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
        }

        public void ToLand(float radius, float startDegree)
        {
            this.radius = radius;
            currDegree = startDegree;
        }

        public void Move(int direct)
        {
            float speedRadial = direct * speedLinear * deltaTime / radius;
            currDegree = (currDegree + speedRadial) / 180 * (float)Math.PI;

            position.x = radius * (float)Math.Sin(currDegree);
            position.y = radius * (float)Math.Cos(currDegree);
        }
    }
}
