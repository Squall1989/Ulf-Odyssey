using Vector2 = UnityEngine.Vector2;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;

namespace Ulf
{
    public class RegisterMovable : IRegister<IMovable>
    {
        int count;

        private Dictionary<int, IMovable> movementDict = new();

        public IMovable GetComponent(int guid)
        {
            return movementDict[guid];
        }

        public void Record(IMovable component)
        {
            movementDict.Add(count++, component);
        }

        public float InteractionDist(int _object, int _subject)
        {
            var objectPos = new Vector2(movementDict[_object].Position.x, movementDict[_object].Position.y);
            var subjectPos = new Vector2(movementDict[_subject].Position.x, movementDict[_subject].Position.y);

            return (objectPos - subjectPos).magnitude;
        }
    }
}
