using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneMembers
{
    public class ZoneRole : ValueObject
    {
        public static ZoneRole Member => new ZoneRole(nameof(Member));
        public static ZoneRole Captain => new ZoneRole(nameof(Captain));
        public static ZoneRole Administrator => new ZoneRole(nameof(Administrator));
        public string Value { get; }
        private ZoneRole(string value)
        {
            Value = value;
        }
        
        private ZoneRole(){}
    }
}