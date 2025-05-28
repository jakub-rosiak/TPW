using System.Collections.ObjectModel;
using Data.Models;
using Data.Repositories;
using Model.Interfaces;
using Logic.Interfaces;
using Logic.Services;
using Logic.Events;

namespace Model.Models;

public class BallSimulationModel : IBallSimulationModel
{
    private readonly IBallLogic _logic;
    private readonly Board _board;
    private readonly ILogger _logger;
    public ObservableCollection<BallDisplay> Balls { get; } = new();

    public event EventHandler? BallsChanged;

    public BallSimulationModel()
    {
        _logger = new Logger();
        _board = new Board(800, 600);
        _logic = new BallLogic(new BallsRepository(), _board, _logger);
        _logic.BallMoved += OnBallMoved;
    }
    
    public void CreateBalls(int count)
    {
        _logic.CreateBalls(count);
        Balls.Clear();
        foreach (var ball in _logic.GetBalls())
        {
            Balls.Add(new BallDisplay(ball.Id, ball.XPos - ball.Radius, ball.YPos - ball.Radius, ball.Radius));
        }
        BallsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartSimulation()
    {
        _logger.Debug("Starting simulation");
        _logic.Start();
    }

    public void StopSimulation()
    {
        _logger.Debug("Stopping simulation");
        _logic.Stop();
    }

    private void OnBallMoved(object? sender, BallMovedEventArgs e)
    {
        var ball = Balls.FirstOrDefault(b => b.Id == e.BallId);
        if (ball != null)
        {
            ball.XPos = e.NewX - ball.Radius;
            ball.YPos = e.NewY - ball.Radius;
        }
    }

}