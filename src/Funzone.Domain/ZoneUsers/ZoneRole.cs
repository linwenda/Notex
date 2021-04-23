using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneUsers
{
    public class ZoneRole : ValueObject
    {
        public static ZoneRole Member => new ZoneRole(nameof(Member));
        public static ZoneRole Moderator => new ZoneRole(nameof(Moderator));
        public static ZoneRole Administrator => new ZoneRole(nameof(Administrator));
        public string Value { get; }
        private ZoneRole(string value)
        {
            Value = value;
        }
        
        private ZoneRole(){}
    }
}