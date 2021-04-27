using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneMembers
{
    public class ZoneMemberRole : ValueObject
    {
        public static ZoneMemberRole Member => new ZoneMemberRole(nameof(Member));
        public static ZoneMemberRole Moderator => new ZoneMemberRole(nameof(Moderator));
        public static ZoneMemberRole Administrator => new ZoneMemberRole(nameof(Administrator));
        public string Value { get; }
        
        private ZoneMemberRole(string value)
        {
            Value = value;
        }
        
        private ZoneMemberRole(){}
    }
}