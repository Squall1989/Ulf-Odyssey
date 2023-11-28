using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Interfaces
{
    public interface IMovable
    {
        float Degree { get; }

        Vector2 Position { get; }

        void SetMoveDirect(int direct);

        void ToLand(Vector2 pos, float radius, float startDegree);
    }
}
