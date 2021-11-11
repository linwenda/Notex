﻿using System;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users.Exceptions;

namespace MarchNote.Domain.Users
{
    public sealed class User : Entity<Guid>
    {
        public DateTime RegisteredAt { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Bio { get; private set; }
        public string Avatar { get; private set; }
        public bool IsActive { get; private set; }

        private User()
        {
            //Only for EF
        }

        private User(string email, string hashedPassword, string firstName, string lastName)
        {
            Id = new Guid();
            RegisteredAt = DateTime.UtcNow;
            Email = email;
            Password = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            IsActive = true;
        }

        public static User Register(
            IUserChecker userChecker,
            IEncryptionService encryptionService,
            string email,
            string password,
            string firstName,
            string lastName)
        {
            if (!userChecker.IsUniqueEmail(email))
            {
                throw new EmailAlreadyExistsException();
            }

            return new User(email,
                encryptionService.HashPassword(password),
                firstName,
                lastName);
        }

        public void ChangePassword(
            IEncryptionService encryptionService,
            string oldPassword,
            string newPassword)
        {
            CheckPassword(encryptionService, oldPassword);
            Password = encryptionService.HashPassword(newPassword);
        }

        public void CheckPassword(IEncryptionService encryptionService, string password)
        {
            if (!encryptionService.VerifyHashedPassword(Password, password))
            {
                throw new IncorrectEmailOrPasswordException();
            }
        }

        public void UpdateProfile(
            string firstName,
            string lastName,
            string bio,
            string avatar)
        {
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
            Avatar = avatar;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}