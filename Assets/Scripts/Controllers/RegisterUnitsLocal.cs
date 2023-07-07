using System.Collections.Generic;

namespace Ulf
{
    public class RegisterUnitsLocal : IRegister<Unit>
    {
        protected int guidCount = 0;
        protected Dictionary<int, Unit> unitsDict = new();

        public Unit GetComponent(int guid)
        {
            if (!unitsDict.ContainsKey(guid))
                return null;

            return unitsDict[guid];
        }

        public void Record(Unit unit)
        {
            unitsDict.Add(guidCount++, unit);
        }

    }
}