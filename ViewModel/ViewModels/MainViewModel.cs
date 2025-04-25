using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViewModel.Commands;
using Model.Interfaces;
using Model.Models;

namespace ViewModel.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private IBallSimulationModel _model;

    public BoardDisplay Board { get; }
    public IBallSimulationModel Model
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged(nameof(Model));
        }
    }

    public ObservableCollection<BallDisplay> Balls => _model.Balls;

    private int _ballCount;

    public int BallCount
    {
        get => _ballCount;
        set
        {
            _ballCount = value;
            OnPropertyChanged(nameof(BallCount));
        }
    }
    
    private RelayCommand _createBallsCommand;
    private RelayCommand _startSimulationCommand;
    private RelayCommand _stopSimulationCommand;

    public ICommand CreateBallsCommand => _createBallsCommand;
    public ICommand StartSimulationCommand => _startSimulationCommand;
    public ICommand StopSimulationCommand => _stopSimulationCommand;


    public MainViewModel()
    {
        _model = new BallSimulationModel();
        
        Board = new BoardDisplay(800, 600);
        
        _startSimulationCommand = new RelayCommand(_ => _model.StartSimulation(), _ => Balls.Any());
        _stopSimulationCommand = new RelayCommand(_ => _model.StopSimulation(), _ => Balls.Any());
        
        _createBallsCommand = new RelayCommand(_ =>
        {
            _model.CreateBalls(BallCount);
            _startSimulationCommand.RaiseCanExecuteChanged();
            _stopSimulationCommand.RaiseCanExecuteChanged();
        }, _ => BallCount > 0);
        
        _ballCount = 5;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}