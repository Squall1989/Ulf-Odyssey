using System;
using Vector2 = UnityEngine.Vector2;

namespace Ulf
{
    public class Bridge : IRound
    {
        private IRoundMono _roundMono;

        public float Size { get; private set; }
        public Vector2 Position { get; private set; }
        public IRoundMono RoundMono => _roundMono;

        private Planet _inPlanet;
        private Planet _outPlanet;

        public Bridge(float size, Vector2 position, Planet inPlanet, IRoundMono roundMono)
        {
            _roundMono = roundMono;
            Size = size;
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
            if (degree < 90)
                return _inPlanet;
            else
                return _outPlanet;
        }
    }
}