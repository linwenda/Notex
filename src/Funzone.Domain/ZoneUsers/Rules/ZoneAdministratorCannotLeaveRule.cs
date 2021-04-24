using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneUsers.Rules
{
    public class ZoneAdministratorCannotLeaveRule : IBusinessRule
    {
        private readonly ZoneRole _zoneRole;

        public ZoneAdministratorCannotLeaveRule(ZoneRole zoneRole)
        {
            _zoneRole = zoneRole;
        }
        
        public bool IsBroken()
        {
            return _zoneRole == ZoneRole.Administrator;
        }

        public string Message => "Administrator cannot leave.";
    }
}