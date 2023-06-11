using Assets.Scripts.Interfaces;
using System.Collections.Generic;

namespace Ulf
{
    public class NPCBehaviours : IRegister<Unit>
    {
        private int count;
        private Dictionary<int, NPCharacter> unitCharacters = new();

        public void Record(Unit unit)
        {
            var character = new NPCharacter(unit, default);
            unitCharacters.Add(count++, character);
        }

        public Unit GetComponent(int guid)
        {
            return unitCharacters[guid].Unit;
        }
    }
}