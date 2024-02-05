
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public interface IRound
    {
        IRoundMono RoundMono {  get; }
        void AddUnit(Unit unit);
        void RmUnit(Unit unit);
    }
}
