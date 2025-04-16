using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : IBall
    {
        public int Id { get; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double Radius { get; }

        public Ball(int id, double x, double y, double radius)
        {
            Id = id;
            XPos = x;
            YPos = y;
            Radius = radius;
        }
    }
}
