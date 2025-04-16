namespace Data
{
    public interface IBallsRepository
    {
        void AddBall(Ball ball);
        void RemoveBall(Ball ball);
        IEnumerable<Ball> GetAllBalls();
    }
}