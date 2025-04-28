using System.Diagnostics;
using Logic.Events;
using Logic.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Logic.Services
{
    public class BallLogic(IBallsRepository repository, IBoard board) : IBallLogic
    {
        private readonly Random _random = new();
        private Thread? _simulationThread;
        private volatile bool _isRunning;
        public event EventHandler<BallMovedEventArgs>? BallMoved;

        public void CreateBalls(int initialCount)
        {
            repository.Clear();
            for (int i = 0; i < initialCount; i++)
            {
                Console.WriteLine($"Creating ball at ({i + 1}/{initialCount})");
                double mass = RandomDouble(20, 100);
                double radius = RandomDouble(10, 30);
                double xPos = _random.NextDouble() * (board.Width - 2 * radius) + radius;
                double yPos = _random.NextDouble() * (board.Height - 2 * radius) + radius;
                double xVel = RandomDouble(-500, 500);
                double yVel = RandomDouble(-500, 500);

                Console.WriteLine($"Creating ball at ({xPos}, {yPos})");
                IBall ball = new Ball(i, xPos, yPos, radius, mass);
                ball.XVel = xVel;
                ball.YVel = yVel;
                repository.AddBall(ball);
            }
        }
        
        public async Task UpdatePositionsAsync(double deltaTime)
        {
            var tasks = new List<Task>();

            foreach (var b1 in repository.GetAllBalls())
            {
                tasks.Add(Task.Run(() =>
                {
                    b1.XPos += b1.XVel * deltaTime;
                    b1.YPos += b1.YVel * deltaTime;

                    if (b1.XPos <= b1.Radius)
                    {
                        b1.XPos = b1.Radius;
                        b1.XVel *= -1;
                    }
                    else if (b1.XPos >= board.Width - b1.Radius)
                    {
                        b1.XPos = board.Width - b1.Radius;
                        b1.XVel *= -1;
                    }

                    if (b1.YPos <= b1.Radius)
                    {
                        b1.YPos = b1.Radius;
                        b1.YVel *= -1;
                    }
                    else if (b1.YPos >= board.Height - b1.Radius)
                    {
                        b1.YPos = board.Height - b1.Radius;
                        b1.YVel *= -1;
                    }

                    foreach (var b2 in repository.GetAllBalls())
                    {
                        if (b1.Equals(b2)) continue;
                        var dx = b1.XPos - b2.XPos;
                        var dy = b1.YPos - b2.YPos;
                        var distanceSquared = dx * dx + dy * dy;
                        var radiusSum = b1.Radius + b2.Radius;

                        if (distanceSquared < radiusSum * radiusSum)
                        {
                            HandleCollision(b1, b2);
                        }
                    }
                
                    BallMoved?.Invoke(this, new BallMovedEventArgs(b1.Id, b1.XPos, b1.YPos));
                }));
            }
            
            await Task.WhenAll(tasks);
        }

        private void HandleCollision(IBall b1, IBall b2)
        {
            Console.WriteLine($"Collision: {b1.Id}, {b2.Id}");
            double dx = b1.XPos - b2.XPos;
            double dy = b1.YPos - b2.YPos;
            double dvx = b1.XVel - b2.XVel;
            double dvy = b1.YVel - b2.YVel;
            
            double distSquared = dx * dx + dy * dy;
            if (distSquared == 0) return;
            
            double dotProduct = dvx * dx + dvy * dy;
            
            if (dotProduct >= 0) return;
            
            double collisionScale = (2 * dotProduct) / ((b1.Mass + b2.Mass) * distSquared);
            double fx = collisionScale * dx;
            double fy = collisionScale * dy;

            b1.XVel -= fx * b2.Mass;
            b1.YVel -= fy * b2.Mass;
            b2.XVel += fx * b1.Mass;
            b2.YVel += fy * b1.Mass;

        }

        public void Start()
        {
            Console.WriteLine("Starting timer");
            Stop();
            
            _isRunning = true;
            _simulationThread = new Thread(() =>
            {
                var sw = new Stopwatch();
                sw.Start();
                double previousTime = 0;

                while (_isRunning)
                {
                    var currentTime = sw.Elapsed.TotalMilliseconds / 1000.0;
                    var deltaTime = currentTime - previousTime;

                    if (deltaTime >= 0.005)
                    {
                        Task.Run(async () => await UpdatePositionsAsync(deltaTime));
                        previousTime = currentTime;
                    }

                    Thread.Sleep(1);
                }
            });
            
            _simulationThread.IsBackground = true;
            _simulationThread.Start();
        }
        
        public void Stop()
        {
            _isRunning = false;
            _simulationThread?.Join(100);
            _simulationThread = null;
        }

        public IEnumerable<IBall> GetBalls()
        {
            return repository.GetAllBalls();
        }
        
        private static double Clamp(double value, double min, double max) => Math.Max(min, Math.Min(max, value));
        
        private double RandomDouble(double min, double max) => _random.NextDouble() * (max - min) + min;
    }
}