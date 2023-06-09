namespace Assets.Scripts.Interfaces
{
    public interface IMovable
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="direct"> 1 - clockwise, -1 - counterclockwise 0 - stop</param>
        /// <returns>x and y pos on planet local</returns>
        (float x, float y) Move(int direct);
    }
}
