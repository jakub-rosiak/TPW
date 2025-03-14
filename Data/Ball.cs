using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball
    {
        public static readonly double Radius = 5;
        public double XPos { get; private set; }
        public double YPos { get; private set; }
        public double XVel { get; private set; }
        public double YVel { get; private set; }
        public Ball(double xPos, double yPos, double xVel, double yVel)
        {
            XPos = xPos;
            YPos = yPos;
            XVel = xVel;
            YVel = yVel;
        }

        public void Move(double deltaTime)
        {
            XPos += XVel * deltaTime;
            YPos += YVel * deltaTime;
        }

        public void AddForce(double xVel, double yVel)
        {
            XVel += xVel;
            YVel += yVel;
        }
    }
}
