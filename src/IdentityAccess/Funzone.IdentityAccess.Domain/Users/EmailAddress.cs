using Ardalis.GuardClauses;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.IdentityAccess.Domain.Users
{
    public class EmailAddress : ValueObject
    {
        public string Address { get; }

        public EmailAddress(string address)
        {
            Guard.Against.NullOrEmpty(address, nameof(address));
            Guard.Against.InvalidFormat(address, nameof(address),
                @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            this.Address = address;
        }

        public override string ToString()
        {
            return $"EmailAddress [address = {Address}]";
        }
    }
}