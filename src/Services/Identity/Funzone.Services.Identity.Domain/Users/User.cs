using System;
using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Identity.Domain.Users.Events;
using Funzone.Services.Identity.Domain.Users.Exceptions;

namespace Funzone.Services.Identity.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordSalt { get; private set; }
        public string PasswordHash { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string Nickname { get; private set; }

        private User()
        {
        }

        private User(string userName, string passwordSalt, string passwordHash, EmailAddress emailAddress)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.UserName = userName;
            this.PasswordSalt = passwordSalt;
            this.PasswordHash = passwordHash;
            this.EmailAddress = emailAddress;
            this.AddDomainEvent(new UserRegisteredDomainEvent(
                this.Id,
                this.UserName,
                this.EmailAddress.Address,
                this.Nickname));
        }

        public static User RegisterWithEmail(
            EmailAddress emailAddress,
            string passwordSalt,
            string passwordHash,
            IUserCounter userCounter)
        {
            if (userCounter.CountUsersWithUserName(emailAddress.Address) > 0)
            {
                throw new UserNameMustBeUniqueException();
            }

            return new User(emailAddress.Address, passwordSalt, passwordHash, emailAddress);
        }

        public void ConfirmEmail()
        {
            if (!this.EmailConfirmed)
                this.EmailConfirmed = true;

            this.AddDomainEvent(new UserEmailConfirmedDomainEvent(
                this.Id,
                this.EmailAddress.Address));
        }
    }
}