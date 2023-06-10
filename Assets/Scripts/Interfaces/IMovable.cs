using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Interfaces
{
    public interface IMovable
    {
        float Degree { get; }

        Vector2 Position { get; }

        void Move(int direct);

        void ToLand(float radius, float startDegree);
    }
}
