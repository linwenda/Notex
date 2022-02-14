namespace SmartNote.Core.Clocks;

public class Clock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}