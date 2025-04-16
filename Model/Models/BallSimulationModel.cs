using System.Collections.ObjectModel;
using Data;
using Model.Interfaces;
using Logic;

namespace Model.Models;

public class BallSimulationModel : IBallSimulationModel
{
    private readonly IBallLogic _logic;
    private readonly Board _board;
    public ObservableCollection<BallDisplay> Balls { get; } = new();

    public event EventHandler? BallsChanged;

    public BallSimulationModel()
    {
        _board = new Board(800, 600);
        _logic = new BallLogic(new BallsRepository(), new Board(800, 600));
        _logic.BallMoved += OnBallMoved;
    }
    
    public void CreateBalls(int count)
    {
        _logic.CreateBalls(count);
        Balls.Clear();
        foreach (var ball in _logic.GetBalls())
        {
            Balls.Add(new BallDisplay(ball.Id, ball.XPos, ball.YPos, ball.Radius));
        }
        BallsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartSimulation(double intervalMilliseconds)
    {
        Console.WriteLine("Starting simulation");
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