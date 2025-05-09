﻿using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Ulf
{

    public class CircleMove : IMovable, IKillable
    {
        private bool isDead = false;

        protected float deltaTime;
        protected IRound _round;
        protected float radius;
        protected float speedLinear;
        protected float currDegree;
        protected Vector2 _position;
        protected int _lookDirect;
        protected int _moveDirect;

        public Action<float> OnChangeSpeed;
        public Action<int> OnMoveDirect;
        private int _restrictDir;

        public float Degree => currDegree;

        public Vector2 PlanetPosition => _round.Position;
        public Vector2 Position => _position;
        public IRound Round => _round;
        public int Direct => _lookDirect;
        public float Speed => speedLinear;

        public CircleMove()
        {

        }

        public void SetDeltaTime(float delta)
        {
            deltaTime = delta;
            Move(_moveDirect);
        }

        protected float linearSpeed => speedLinear * deltaTime * (float)Math.PI * 2f / radius;

        protected virtual void Move(int moveDirect)
        {
            float speedRadial = moveDirect * linearSpeed;
            currDegree += speedRadial;

            if (currDegree >= 360f)
                currDegree -= 360f;

            if (currDegree < 0)
                currDegree += 360f;

            _position = GetMovePos(_round.Position, radius, currDegree);
        }

        public virtual void ToLand(IRound round, float startAngle)
        {
            _round = round;
            this.radius = round.Radius;
            currDegree = startAngle;
        }

        public void SetSpeed(float speed)
        {
            if(_restrictDir == _moveDirect)
            {
                speed = 0;
            }
            speedLinear = speed;
            OnChangeSpeed?.Invoke(speed);
        }
        public void SetAngle(float angle)
        {
            
            currDegree = angle;
        }

        public void SetMoveDirect(int direct)
        {
            _moveDirect = direct;
            if (direct != 0)
            {
                _lookDirect = direct;
            }
            OnMoveDirect?.Invoke(direct);
        }

        public virtual void MoveCommand(MovementAction action)
        {
            if (isDead)
            {
                return;
            }
            SetAngle(action.fromAngle);
            SetMoveDirect(action.direction);
            SetSpeed(action.speed);
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

        internal void RestrictMove(int dir)
        {
            if (dir == Direct)
            {
                SetSpeed(0);
            }
            _restrictDir = dir;
        }

        public void Kill()
        {
            isDead = true;
            SetSpeed(0);
        }

        public void Ressurect()
        {
            isDead = false;
        }
    }
}
