using Vector2 = UnityEngine.Vector2;
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public interface IRound
    {
        public float Radius { get; }
        public Vector2 Position { get; }
        IRoundMono RoundMono {  get; }
        void AddUnit(Unit unit);
        void RmUnit(Unit unit);
    }
}
