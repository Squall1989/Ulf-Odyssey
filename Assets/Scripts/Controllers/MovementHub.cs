
using Assets.Scripts.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Controllers
{
    public class MovementHub
    {
        private Dictionary<int, IControl> controlDict = new ();
        private Dictionary<int, IMovable> movementDict = new ();

        public void RegControl(int guid, IControl control)
        {
            controlDict.Add(guid, control);
            control.OnMove += (dir) => movementDict[guid].Move(dir);
        }

        public void RegMovable(int guid, IMovable movable)
        {
            movementDict.Add(guid, movable);
        }
    }
}
