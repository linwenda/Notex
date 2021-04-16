using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public class ZoneStatus : ValueObject
    {
        public static ZoneStatus ToBeReviewed => new ZoneStatus(nameof(ToBeReviewed));
        
        public ZoneStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}