using System.ComponentModel;

namespace Model;

public class BallDisplay : INotifyPropertyChanged
{
    public int Id { get; }

    private double _xPos;

    public double XPos
    {
        get => _xPos;
        set { _xPos = value;
            OnPropertyChanged(nameof(XPos));
        }
    }
    
    private double _yPos;

    public double YPos
    {
        get => _yPos;
        set { _yPos = value; OnPropertyChanged(nameof(YPos)); }
    }
    
    public double Radius { get; }

    public BallDisplay(int id, double xPos, double yPos, double radius)
    {
        Id = id;
        Radius = radius;
        _xPos = xPos;
        _yPos = yPos;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}