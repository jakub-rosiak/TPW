using Data.Interfaces;

namespace Data.Models
{
    public class Ball : IBall
    {
        public int Id { get; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double XVel { get; set; }
        public double YVel { get; set; }
        public double Radius { get; }
        public double Mass { get; }

        public Ball(int id, double x, double y, double radius, double mass)
        {
            Id = id;
            XPos = x;
            YPos = y;
            Radius = radius;
            Mass = mass;
            XVel = 0;
            YVel = 0;
        }

        public void AddForce(double x, double y)
        {
            XVel += x;
            YVel += y;
        }

        public void Move(double deltaTime)
        {
            XPos += XVel * deltaTime;
            YPos += YVel * deltaTime;
        }
    }
}
