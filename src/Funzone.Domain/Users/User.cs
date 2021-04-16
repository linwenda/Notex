using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users.Events;
using Funzone.Domain.Users.Rules;

namespace Funzone.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordSalt { get; private set; }
        public string PasswordHash { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public string NickName { get; private set; }

        private User()
        {
        }

        private User(
            IUserChecker userCounter,
            string userName,
            string passwordSalt,
            string passwordHash,
            EmailAddress emailAddress)
        {
            CheckRule(new EmailMustBeUniqueRule(userCounter, emailAddress));

            Id = new UserId(Guid.NewGuid());
            UserName = userName;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            EmailAddress = emailAddress;

            AddDomainEvent(new UserRegisteredDomainEvent(
                Id,
                UserName,
                EmailAddress.Address,
                NickName));
        }

        public static User RegisterByEmail(
            IUserChecker userChecker,
            EmailAddress emailAddress,
            string passwordSalt,
            string passwordHash)
        {
            return new User(
                userChecker,
                emailAddress.Address,
                passwordSalt,
                passwordHash,
                emailAddress);
        }
    }
}