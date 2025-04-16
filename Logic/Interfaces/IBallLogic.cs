namespace Logic
{
    public interface IBallLogic
    {
        event EventHandler<BallMovedEventArgs> BallMoved; 
        void CreateBalls(int initialCount);
        IEnumerable<Data.Ball> GetBalls();
        void Start(double intervalInMilliseconds);
        void Stop();
    }
}