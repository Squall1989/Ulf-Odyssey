
using Assets.Scripts.Interfaces;

namespace Ulf
{
    public interface IRound
    {
        public void NewMovable(IMovable movable, float startDegree);
        public void RmMovable(IMovable movable);
    }
}
