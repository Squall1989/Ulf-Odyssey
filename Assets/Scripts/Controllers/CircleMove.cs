using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Ulf
{
    public class CircleMove : IMovable
    {
        protected float deltaTime;
        protected IRound _round;
        protected float radius;
        protected float speedLinear;
        protected float currDegree;
        protected Vector2 _position;
        protected int _direct;

        public float Degree => currDegree;

        public Vector2 PlanetPosition => _round.Position;
        public Vector2 Position => _position;
        public IRound Round => _round;

        public CircleMove(float speed)
        {
            speedLinear = speed;
        }

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
            Move(_direct);
        }

        protected virtual void Move(int moveDirect)
        {
            float speedRadial = moveDirect * speedLinear * deltaTime * (float)Math.PI * 2f  / radius;
            currDegree += speedRadial;

            if (currDegree >= 360f)
                currDegree -= 360f;

            if (currDegree < 0)
                currDegree += 360f;

            _position = GetMovePos(_round.Position, radius, currDegree);
        }

        public void ToLand(IRound round, float startAngle)
        {
            _round = round;
            this.radius = round.Radius;
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

        public static float GetAngle(Vector2 relativePointPos)
        {
            var tan2 = Mathf.Atan2(relativePointPos.y, relativePointPos.x);
            var angle = tan2 * 180f / Mathf.PI;

            if(angle < 0)
            {
                angle += 360f;
            }
            
            return angle;
        }

    }
}
