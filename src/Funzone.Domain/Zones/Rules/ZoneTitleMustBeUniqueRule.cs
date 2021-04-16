using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneTitleMustBeUniqueRule : IBusinessRule
    {
        private readonly IZoneCounter _zoneCounter;
        private readonly string _title;

        public ZoneTitleMustBeUniqueRule(IZoneCounter zoneCounter,string title)
        {
            _zoneCounter = zoneCounter;
            _title = title;
        }
        
        public bool IsBroken()
        {
            return _zoneCounter.CountZoneWithTitle(_title) > 0;
        }

        public string Message => "Zone title must be unique.";
    }
}