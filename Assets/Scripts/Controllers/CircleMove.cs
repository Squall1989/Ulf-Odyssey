using Assets.Scripts.Interfaces;
using System;

namespace Ulf
{
    public class CircleMove : IMovable
    {
        private float deltaTime;
        private float radius;
        private float speedLinear;
        private float currDegree;

        private (float x, float y) position;

        public float Degree => currDegree;

        public (float x, float y) Position => position;

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
