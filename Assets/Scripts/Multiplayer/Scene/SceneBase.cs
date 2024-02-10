
using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ulf
{
    public class SceneBase
    {
        protected List<Planet> planets = new();
        protected List<IRound> rounds = new();

        public void AddPlanet(Planet planet)
        {
            planets.Add(planet);
            rounds.Add(planet);
        }

        public void AddBridge(Bridge bridge)
        {
            rounds.Add(bridge);
        }

        

        public IRound GetRoundFromId(int id)
        {
            return rounds.FirstOrDefault(p => p.ID == id);
        }
    }
}