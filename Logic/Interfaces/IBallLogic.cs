namespace Logic
{
    public interface IBallLogic
    {
        event EventHandler<BallMovedEventArgs> BallMoved; 
        void CreateBalls(int initialCount, double width, double height);
        IEnumerable<Data.Ball> GetBalls();
        void Start(double intervalInMilliseconds);
        void Stop();
    }
}