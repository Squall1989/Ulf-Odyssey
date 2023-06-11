namespace Ulf
{
    public interface IRegister<T>
    {
        void Record(T component);
        T GetComponent(int guid);
    }
}