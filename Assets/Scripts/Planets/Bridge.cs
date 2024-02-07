using System;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Bridge : IRound
    {
        public float Radius { get; private set; }
        private IRoundMono _roundMono;
        public Vector2 Position { get; private set; }
        public IRoundMono RoundMono => _roundMono;

        private Planet _inPlanet;
        private Planet _outPlanet;
        private const float allowance = 10f;

        public Bridge(float size, Vector2 position, Planet inPlanet, IRoundMono roundMono)
        {
            Radius = size;
            _roundMono = roundMono;
            Position = position;
            _inPlanet = inPlanet;
        }

        public void ConnectOutPlanet(Planet outPlanet)
        {
            _outPlanet = outPlanet;

        }

        public void AddUnit(Unit unit)
        {

        }

        public void RmUnit(Unit unit)
        {

        }

        internal Planet GetOutPlanet(float degree)
        {
            if (IsUpperAllow(degree))
            {
                return _outPlanet;
            }
            else if (IsLowerAllow(degree))
            {
                return _inPlanet;
            }
            else
            {
                return null;
            }
        }

        public static bool IsStandableBridgeDegree(float degree)
        {
            if(IsUpperAllow(degree)) 
                return true;
            if(IsLowerAllow(degree)) 
                return true;

            return false;
        }

        private static bool IsUpperAllow(float degree)
        {
            const float upperDeg = 90f;

            if (MathF.Abs(degree - upperDeg) <= allowance)
                return true;
            else
                return false;
        }

        private static bool IsLowerAllow(float degree)
        {
            const float lowerDeg = 270f;

            if (MathF.Abs(degree - lowerDeg) <= allowance)
                return true;
            else
                return false;
        }
    }
}