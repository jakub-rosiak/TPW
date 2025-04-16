using Data;
using System;
using Timer = System.Timers.Timer;

namespace Logic
{
    public class BallLogic : IBallLogic
    {
        private readonly IBallsRepository _repository;
        private readonly Random _random = new();
        private Timer? _timer;

        private double _boardWidth;
        private double _boardHeight;
        
        public event EventHandler<BallMovedEventArgs>? BallMoved;

        public BallLogic(IBallsRepository repository)
        {
            _repository = repository;
        }

        public void CreateBalls(int initialCount, double width, double height)
        {
            _boardWidth = width;
            _boardHeight = height;

            double radius = 10;
            for (int i = 0; i < initialCount; i++)
            {
                Console.WriteLine($"Creating ball at ({i + 1}/{initialCount})");
                double xPos = _random.NextDouble() * (width - 2 * radius) + radius;
                double yPos = _random.NextDouble() * (height - 2 * radius) + radius;

                Console.WriteLine($"Creating ball at ({xPos}/{yPos})");
                var ball = new Ball(i, xPos, yPos, radius);
                _repository.AddBall(ball);
            }
        }
        
        public void UpdatePositions(double deltaTime)
        {
            foreach (var ball in _repository.GetAllBalls())
            {
                Console.WriteLine("Updating ball positions");
                double dx = _random.NextDouble() * 10 - 5;
                double dy = _random.NextDouble() * 10 - 5;
                
                ball.XPos = Clamp(ball.XPos + dx, ball.Radius, _boardWidth - ball.Radius);
                ball.YPos = Clamp(ball.XPos + dy, ball.Radius, _boardHeight - ball.Radius);
                
                BallMoved?.Invoke(this, new BallMovedEventArgs(ball.Id, ball.XPos, ball.YPos));
            }
        }

        public void Start(double intervalInMilliseconds)
        {
            _timer = new Timer(intervalInMilliseconds);
            _timer.Elapsed += (sender, e) => UpdatePositions(0.1);
            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }

        public IEnumerable<Ball> GetBalls()
        {
            return _repository.GetAllBalls();
        }
        
        private static double Clamp(double value, double min, double max) => Math.Max(min, Math.Min(max, value));
    }
}