using Data.Interfaces;

namespace Data.Repositories
{
    public class BallsRepository : IBallsRepository
    {
        private readonly List<IBall> _balls = new();
        private readonly object _lock = new object();

        public void AddBall(IBall ball)
        {
            lock (_lock)
            {
                _balls.Add(ball);
            }
        }

        public void RemoveBall(IBall ball)
        {
            lock (_lock)
            { 
                _balls.Remove(ball);
            }
        }

        public IEnumerable<IBall> GetAllBalls()
        {
            lock (_lock)
            {
                return _balls.ToList();
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _balls.Clear();
            }
        }
    }
}