using Assets.Scripts.Interfaces;
using System;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class CircleMove : IMovable
    {
        protected float deltaTime;
        protected float radius;
        protected float speedLinear;
        protected float currDegree;

        protected Vector2 _planetPos;
        protected Vector2 _position;
        protected int _direct;

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

        protected void Move()
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
