using Data.Interfaces;

namespace Data.Repositories
{
    public class BallsRepository : IBallsRepository
    {
        private readonly List<IBall> _balls = new();

        public void AddBall(IBall ball)
        {
            _balls.Add(ball);
        }

        public void RemoveBall(IBall ball)
        {
            _balls.Remove(ball);
        }

        public IEnumerable<IBall> GetAllBalls()
        {
            return _balls;
        }

        public void Clear()
        {
            _balls.Clear();
        }
    }
}