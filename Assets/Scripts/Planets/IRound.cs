
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public interface IRound
    {
        void NewUnit(Unit unit, float startDegree);
        void RmUnit(Unit unit);
    }
}
