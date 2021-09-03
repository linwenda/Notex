using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Users
{
    public class User : Entity
    {
        public UserId Id { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public string Email { get; private set; }
        public string NickName { get; private set; }
        public string Password { get; private set; }
        public string Bio { get; private set; }
        public bool IsActive { get; private set; }

        private User()
        {
            //Only for EF
        }

        private User(string email, string hashedPassword)
        {
            Id = new UserId(Guid.NewGuid());
            RegisteredAt = DateTime.UtcNow;
            Email = email;
            Password = hashedPassword;
            IsActive = true;
        }

        public static User Register(
            IUserChecker userChecker,
            IEncryptionService encryptionService,
            string email,
            string password)
        {
            if (!userChecker.IsUniqueEmail(email))
            {
                throw new BusinessException(ExceptionCode.UserEmailExists, "Email already exists");
            }

            return new User(email, encryptionService.HashPassword(password));
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
                throw new BusinessException(ExceptionCode.UserPasswordIncorrect, "Password incorrect");
            }
        }

        public void UpdateProfile(IUserChecker userChecker, string nickName, string bio)
        {
            if (NickName != nickName &&
                string.IsNullOrWhiteSpace(nickName) &&
                !userChecker.IsUniqueNickName(nickName))
            {
                throw new BusinessException(ExceptionCode.UserNickNameExists, "Name already exists");
            }

            NickName = nickName;
            Bio = bio;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}