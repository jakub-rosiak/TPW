using System.ComponentModel;

namespace Model.Models;

public class BoardDisplay : INotifyPropertyChanged
{
    private double _width;
    public double Width
    {
        get => _width;
        set { _width = value; OnPropertyChanged(nameof(Width)); }
    }

    private double _height;
    public double Height
    {
        get => _height;
        set { _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public BoardDisplay(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}