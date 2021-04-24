using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneUsers
{
    public class ZoneUserRole : ValueObject
    {
        public static ZoneUserRole Member => new ZoneUserRole(nameof(Member));
        public static ZoneUserRole Moderator => new ZoneUserRole(nameof(Moderator));
        public static ZoneUserRole Administrator => new ZoneUserRole(nameof(Administrator));
        public string Value { get; }
        private ZoneUserRole(string value)
        {
            Value = value;
        }
        
        private ZoneUserRole(){}
    }
}