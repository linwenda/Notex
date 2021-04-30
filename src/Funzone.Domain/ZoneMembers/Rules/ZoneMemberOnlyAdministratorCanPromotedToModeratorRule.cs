using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.ZoneMembers.Rules
{
    public class ZoneMemberOnlyAdministratorCanPromotedToModeratorRule : IBusinessRule
    {
        private readonly ZoneMember _member;

        public ZoneMemberOnlyAdministratorCanPromotedToModeratorRule(ZoneMember member)
        {
            _member = member;
        }

        public bool IsBroken()
        {
            return _member.Role != UserRole.Administrator;
        }

        public string Message => "Only Administrator can be promoted to moderator.";
    }
}