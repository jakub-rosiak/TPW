using Data.Interfaces;

namespace Data.Models;

public class Board : IBoard
{
    public double Width { get; }
    public double Height { get; }

    public Board(double width, double height)
    {
        Width = width;
        Height = height;
    }
}