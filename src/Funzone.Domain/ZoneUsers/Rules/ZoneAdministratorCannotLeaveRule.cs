using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneUsers.Rules
{
    public class ZoneAdministratorCannotLeaveRule : IBusinessRule
    {
        private readonly ZoneUserRole _zoneRole;

        public ZoneAdministratorCannotLeaveRule(ZoneUserRole zoneRole)
        {
            _zoneRole = zoneRole;
        }
        
        public bool IsBroken()
        {
            return _zoneRole == ZoneUserRole.Administrator;
        }

        public string Message => "Administrator cannot leave.";
    }
}