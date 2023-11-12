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

        private Vector2 _planetPos;
        private Vector2 _position;

        public float Degree => currDegree;

        public Vector2 Position => _position;

        public CircleMove(float speed)
        {
            speedLinear = speed;
        }

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
        }

        public void ToLand(Vector2 pos, float radius, float startAngle)
        {
            _planetPos = pos;
            this.radius = radius;
            currDegree = startAngle;
        }

        public void Move(int direct)
        {
            float speedRadial = direct * speedLinear * deltaTime / radius;
            currDegree = (currDegree + speedRadial) / 180 * (float)Math.PI;

            _position.x = radius * (float)Math.Sin(currDegree);
            _position.y = radius * (float)Math.Cos(currDegree);
            _position += _planetPos;

            if (currDegree >= 360f)
                currDegree -= 360f;
        }
    }
}
