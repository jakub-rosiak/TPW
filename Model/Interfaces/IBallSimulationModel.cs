using System.Collections.ObjectModel;

namespace Model.Interfaces;

public interface IBallSimulationModel
{
    void CreateBalls(int count);
    void StartSimulation(double intervalMilliseconds);
    void StopSimulation();

    ObservableCollection<BallDisplay> Balls { get; }

    event EventHandler? BallsChanged;
}