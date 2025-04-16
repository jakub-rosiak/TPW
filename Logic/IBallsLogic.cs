namespace Logic
{
    public interface IBallsLogic
    {
        void CreateBalls(int initialCount, double width, double height);
        void UpdatePositions(double deltaTime, double width, double height);
        IEnumerable<Data.Ball> GetBalls();
    }
}