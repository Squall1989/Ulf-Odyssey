using System;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Bridge : IRound
    {
        public int ID => (_inPlanet.ID +1) * 100 + _outPlanet.ID;
        public float Radius { get; private set; }
        private IRoundMono _roundMono;
        public Vector2 Position { get; private set; }
        public IRoundMono RoundMono => _roundMono;

        private bool _leftSide;
        private Planet _inPlanet;
        private Planet _outPlanet;
        private const float allowance = 30f;

        public Bridge(bool leftSide, float size, Vector2 position, Planet inPlanet, IRoundMono roundMono)
        {
            _leftSide = leftSide;
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
            if (IsUpperAllow(degree, _leftSide))
            {
                return _outPlanet;
            }
            else if (IsLowerAllow(degree, _leftSide))
            {
                return _inPlanet;
            }
            else
            {
                return null;
            }
        }

        public bool IsStandableBridgeDegree(float degree, bool isToStand)
        {
            if(IsUpperAllow(degree, isToStand)) 
                return true;
            if(IsLowerAllow(degree, isToStand)) 
                return true;

            return false;
        }

        private bool IsUpperAllow(float degree, bool isToStand)
        {
            const float upperDeg = 90f;

            if (MathF.Abs(degree - upperDeg) <= allowance)
            {
                bool isStandLeft = isToStand && _leftSide;

                if (degree > upperDeg && isStandLeft || degree < upperDeg && !isStandLeft)
                    return true;
                else 
                    return false;
            }
            else
                return false;
        }

        private bool IsLowerAllow(float degree, bool isToStand)
        {
            const float lowerDeg = 270f;

            if (MathF.Abs(degree - lowerDeg) <= allowance)
            {
                bool isStandLeft = isToStand && _leftSide;

                if (degree < lowerDeg && isStandLeft || degree > lowerDeg && !isStandLeft)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}