using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Identity.Domain.Users.Events;
using Funzone.Services.Identity.Domain.Users.Rules;
using System;
using System.Collections.Generic;
using Funzone.Services.Identity.Domain.UserRoles;

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
        
        public List<UserRole> Roles { get; private set; }

        private User()
        {
        }

        private User(
            IUserCounter userCounter,
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

            Roles ??= new List<UserRole>
             {
                 UserRole.Guest
             };

            AddDomainEvent(new UserRegisteredDomainEvent(
                Id,
                UserName,
                EmailAddress.Address,
                Nickname));
        }

        public static User RegisterWithEmail(
            IUserCounter userCounter,
            EmailAddress emailAddress,
            string passwordSalt,
            string passwordHash)
        {
            return new User(
                userCounter,
                emailAddress.Address,
                passwordSalt,
                passwordHash,
                emailAddress);
        }
    }
}