namespace Assets.Scripts.Interfaces
{
    public interface IMovable
    {
        float Degree { get; }

        (float x, float y) Position { get; }

        void Move(int direct);

        void ToLand(float radius, float startDegree);
    }
}
