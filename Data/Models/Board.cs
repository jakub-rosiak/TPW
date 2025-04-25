using Data.Interfaces;

namespace Data.Models;

public class Board : IBoard
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Board(double width, double height)
    {
        Width = width;
        Height = height;
    }
}