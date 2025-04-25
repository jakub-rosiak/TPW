namespace Data.Interfaces
{
    public interface IBallsRepository
    {
        void AddBall(IBall ball);
        void RemoveBall(IBall ball);
        IEnumerable<IBall> GetAllBalls();
        void Clear();
    }
}