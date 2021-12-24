namespace SmartNote.Application.Configuration.Commons
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}