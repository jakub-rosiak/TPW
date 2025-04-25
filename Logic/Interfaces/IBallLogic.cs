using Data.Interfaces;
using Logic.Events;

namespace Logic.Interfaces
{
    public interface IBallLogic
    {
        event EventHandler<BallMovedEventArgs> BallMoved; 
        void CreateBalls(int initialCount);
        IEnumerable<IBall> GetBalls();
        void Start();
        void Stop();
    }
}