using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneMustBeActivatedRule : IBusinessRule
    {
        private readonly ZoneStatus _status;

        public ZoneMustBeActivatedRule(ZoneStatus status)
        {
            _status = status;
        }
        
        public bool IsBroken()
        {
            return _status != ZoneStatus.Active;
        }

        public string Message => "Zone was closed";
    }
}