namespace Data
{
    public class BallsRepository : IBallsRepository
    {
        private readonly List<Ball> _balls = new();

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
        }

        public IEnumerable<Ball> GetAllBalls()
        {
            return _balls;
        }

        public void Clear()
        {
            _balls.Clear();
        }
    }
}