using System.Collections.ObjectModel;
using Data;
using Model.Interfaces;
using Logic;

namespace Model.Models;

public class BallSimulationModel : IBallSimulationModel
{
    private readonly IBallLogic _logic;
    public ObservableCollection<BallDisplay> Balls { get; } = new();

    public event EventHandler? BallsChanged;

    public BallSimulationModel()
    {
        _logic = new BallLogic(new BallsRepository());
        _logic.BallMoved += OnBallMoved;
    }
    
    public void CreateBalls(int count)
    {
        _logic.CreateBalls(count, 800, 600);
        Balls.Clear();
        foreach (var ball in _logic.GetBalls())
        {
            Balls.Add(new BallDisplay(ball.Id, ball.XPos, ball.YPos, ball.Radius));
        }
        BallsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartSimulation(double intervalMilliseconds)
    {
        _logic.Start(intervalMilliseconds);
    }

    public void StopSimulation() => _logic.Stop();

    private void OnBallMoved(object? sender, BallMovedEventArgs e)
    {
        var ball = Balls.FirstOrDefault(b => b.Id == e.BallId);
        if (ball != null)
        {
            ball.XPos = e.NewX;
            ball.YPos = e.NewY;
        }
    }
}