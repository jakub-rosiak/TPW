namespace Logic.Events;

public class BallMovedEventArgs : EventArgs
{
    public int BallId { get; }
    public double NewX { get; }
    public double NewY { get; }

    public BallMovedEventArgs(int id, double x, double y)
    {
        BallId = id;
        NewX = x;
        NewY = y;
    }
}