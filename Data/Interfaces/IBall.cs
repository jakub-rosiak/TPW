namespace Data;

public interface IBall
{ 
    int Id { get; }
    double XPos { get; set; }
    double YPos { get; set; }
    double Radius { get; }
}