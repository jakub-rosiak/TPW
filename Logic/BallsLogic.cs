using Data;
using System;
using Timer = System.Timers.Timer;

namespace Logic
{
    public class BallsLogic : IBallsLogic
    {
        private readonly IBallsRepository _repository;
        private readonly Random _random = new();
        private Timer? _timer;

        public BallsLogic(IBallsRepository repository)
        {
            _repository = repository;
        }

        public void CreateBalls(int initialCount, double width, double height)
        {
            for (int i = 0; i < initialCount; i++)
            {
                double xPos = _random.NextDouble() * (width - 2 * Ball.Radius);
                double yPos = _random.NextDouble() * (height - 2 * Ball.Radius);
                double xVel = _random.NextDouble() * 10 - 5;
                double yVel = _random.NextDouble() * 10 - 5;

                var ball = new Ball(xPos + Ball.Radius, yPos + Ball.Radius, xVel, yVel);
                _repository.AddBall(ball);
            }
        }

        public void UpdatePositions(double deltaTime, double width, double height)
        {
            foreach (var ball in _repository.GetAllBalls())
            {
                ball.Move(deltaTime);

                if (ball.XPos < Ball.Radius || ball.XPos > width - Ball.Radius)
                {
                    ball.AddForce(-2 * ball.XVel, 0); 
                }
                if (ball.YPos < Ball.Radius || ball.YPos > height - Ball.Radius)
                {
                    ball.AddForce(0, -2 * ball.YVel);
                }
            }
        }

        public void StartAutoUpdate(double intervalInMilliseconds, double width, double height)
        {
            _timer = new Timer(intervalInMilliseconds);
            _timer.Elapsed += (sender, e) => UpdatePositions(0.1, width, height);
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void StopAutoUpdate()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }

        public IEnumerable<Ball> GetBalls()
        {
            return _repository.GetAllBalls();
        }
    }
}