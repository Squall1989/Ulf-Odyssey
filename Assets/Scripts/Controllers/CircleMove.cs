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
        private int _direct;

        public float Degree => currDegree;

        public Vector2 PlanetPosition => _planetPos;
        public Vector2 Position => _position;

        public CircleMove(float speed)
        {
            speedLinear = speed;
        }

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
            Move();
        }

        private void Move()
        {
            float speedRadial = _direct * speedLinear  / radius;
            currDegree += (float)(speedRadial * Math.PI / 180f);

            if (currDegree >= 360f)
                currDegree -= 360f;

            _position = GetMovePos(_planetPos, radius, currDegree);

            
        }

        public void ToLand(Vector2 pos, float radius, float startAngle)
        {
            _planetPos = pos;
            this.radius = radius;
            currDegree = startAngle;
        }

        public void SetAngle(float angle)
        {
            if(angle != currDegree)
            {

            }
            currDegree = angle;
        }

        public void SetMoveDirect(int direct)
        {
            _direct = direct;
        }

        public static Vector2 GetMovePos(Vector2 movePlanetPos, float moveRadius, float moveDegree)
        {
            float x = movePlanetPos.x + moveRadius * (float)Math.Cos(moveDegree * Math.PI / 180f);
            float y = movePlanetPos.y + moveRadius * (float)Math.Sin(moveDegree * Math.PI / 180f);
            return new Vector2(x,y);

        }
    }
}
