using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public class ZoneStatus : ValueObject
    {
        public string Value { get; }

        public static ZoneStatus Active => new ZoneStatus(nameof(Active));
        public static ZoneStatus Closed => new ZoneStatus(nameof(Closed));

        private ZoneStatus(string value)
        {
            Value = value;
        }
    }
}