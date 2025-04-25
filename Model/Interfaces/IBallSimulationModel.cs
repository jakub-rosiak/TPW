using System.Collections.ObjectModel;
using Model.Models;

namespace Model.Interfaces;

public interface IBallSimulationModel
{
    void CreateBalls(int count);
    void StartSimulation();
    void StopSimulation();

    ObservableCollection<BallDisplay> Balls { get; }

    event EventHandler? BallsChanged;
}