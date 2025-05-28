using System.Collections.Concurrent;
using System.Diagnostics;
using Logic.Events;
using Logic.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Logic.Services
{
    public class BallLogic(IBallsRepository repository, IBoard board, ILogger logger) : IBallLogic
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
                logger.Info($"Creating ball at ({i + 1}/{initialCount})");
                double mass = RandomDouble(20, 50);
                double radius = mass / 2;
                double xPos = _random.NextDouble() * (board.Width - 2 * radius) + radius;
                double yPos = _random.NextDouble() * (board.Height - 2 * radius) + radius;
                double xVel = RandomDouble(-500, 500);
                double yVel = RandomDouble(-500, 500);
                
                IBall ball = new Ball(i, xPos, yPos, radius, mass);
                ball.XVel = xVel;
                ball.YVel = yVel;
                repository.AddBall(ball);
            }
        }
        
        public async Task UpdatePositionsAsync(double deltaTime)
        {
            var allBalls = repository.GetAllBalls().ToList();
            await Task.Run(() =>
            {
                Parallel.ForEach(allBalls, b1 =>
                {
                    lock (b1)
                    {
                        b1.Move(deltaTime);
                        HandleBoundaryCollision(b1);
                        BallMoved?.Invoke(this, new BallMovedEventArgs(b1.Id, b1.XPos, b1.YPos));
                    }
                });

                Parallel.For(0, allBalls.Count, i =>
                {
                    for (int j = i + 1; j < allBalls.Count; j++)
                    {
                        var b1 = allBalls[i];
                        var b2 = allBalls[j];

                        lock (b1)
                        lock (b2)
                        {
                            if (IsColliding(b1, b2))
                            {
                                logger.Debug($"Ball {b1.Id} collided with {b2.Id}.");
                                HandleCollision(b1, b2);
                            }
                        }
                    }
                });
            });
        }

        private void HandleBoundaryCollision(IBall b1)
        {
            if (b1.XPos <= b1.Radius)
            {
                logger.Debug($"Ball {b1.Id} collided with the left wall");
                b1.XPos = b1.Radius;
                b1.XVel *= -1;
            }
            else if (b1.XPos >= board.Width - b1.Radius)
            {
                logger.Debug($"Ball {b1.Id} collided with the right wall");
                b1.XPos = board.Width - b1.Radius;
                b1.XVel *= -1;
            }

            if (b1.YPos <= b1.Radius)
            {
                logger.Debug($"Ball {b1.Id} collided with the top wall");
                b1.YPos = b1.Radius;
                b1.YVel *= -1;
            }
            else if (b1.YPos >= board.Height - b1.Radius)
            {
                logger.Debug($"Ball {b1.Id} collided with the bottom wall");
                b1.YPos = board.Height - b1.Radius;
                b1.YVel *= -1;
            }
        }

        private bool IsColliding(IBall b1, IBall b2)
        {
            var dx = b1.XPos - b2.XPos;
            var dy = b1.YPos - b2.YPos;
            var distanceSquared = dx * dx + dy * dy;
            var radiusSum = b1.Radius + b2.Radius;
            return distanceSquared < radiusSum * radiusSum;
        }

        private void HandleCollision(IBall b1, IBall b2)
        {
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

            var b1XVel= -(fx * b2.Mass);
            var b1YVel = -(fy * b2.Mass);
            var b2XVel = fx * b1.Mass;
            var b2YVel = fy * b1.Mass;
            b1.AddForce(b1XVel, b1YVel);
            b2.AddForce(b2XVel, b2YVel);

        }

        public void Start()
        {
            logger.Info("Starting timer");
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
            logger.Info("Stopping timer");
            _isRunning = false;
            _simulationThread?.Join(100);
            _simulationThread = null;
        }

        public IEnumerable<IBall> GetBalls()
        {
            return repository.GetAllBalls();
        }
        
        private double RandomDouble(double min, double max) => _random.NextDouble() * (max - min) + min;
    }
}