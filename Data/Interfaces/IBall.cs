namespace Data.Interfaces;

public interface IBall
{ 
    int Id { get; }
    double XPos { get; set; }
    double YPos { get; set; }
    double XVel { get; set; }
    double YVel { get; set; }
    double Radius { get; }
    double Mass { get; }
}