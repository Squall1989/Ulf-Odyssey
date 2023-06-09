
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using Zenject;

namespace Ulf
{
    public class Planet : IRound
    {
        private float radius;
        private List<IMovable> movables;

        public Planet(float radius)
        {
            this.radius = radius;
            movables = new List<IMovable>();
        }

        [Inject]
        public void Init(IRegister<IRound> registerPlanet)
        {
            registerPlanet.Record(this);
        }

        public virtual void NewMovable(IMovable movable, float startDegree)
        {
            movables.Add(movable);
            movable.ToLand(radius, startDegree);
        }

        public void RmMovable(IMovable movable)
        {
            movables.Remove(movable);
        }
    }
}