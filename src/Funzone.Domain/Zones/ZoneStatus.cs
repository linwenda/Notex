using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public class ZoneStatus : ValueObject
    {
        public static ZoneStatus Active => new ZoneStatus(nameof(Active));
        public static ZoneStatus Closed => new ZoneStatus(nameof(Closed));
        
        public ZoneStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}