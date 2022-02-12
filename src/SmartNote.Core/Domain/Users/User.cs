namespace SmartNote.Core.Domain.Users;

public class User : AggregateRoot<Guid>, IHasCreator, IHasCreationTime, ISoftDelete
{
    public Guid CreatorId { get; }
    public DateTime CreationTime { get; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Bio { get; private set; }
    public string Avatar { get; private set; }
    public bool IsDeleted { get; set; }

    private User()
    {
        //Only for EF
    }

    private User(string email, string hashedPassword, string firstName, string lastName)
    {
        Email = email;
        HashedPassword = hashedPassword;
        FirstName = firstName;
        LastName = lastName;
    }

    public static async Task<User> RegisterAsync(
        IUserManager userChecker,
        string email,
        string password,
        string firstName,
        string lastName)
    {
        if (!await userChecker.IsUniqueEmailAsync(email))
        {
            throw new EmailAlreadyExistsException();
        }

        return new User(email,
            password,
            firstName,
            lastName);
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

    public void ChangePassword(IUserManager userManager, string oldPassword, string newPassword)
    {
        if (!userManager.VerifyHashedPassword(HashedPassword, oldPassword))
        {
            throw new IncorrectPasswordException();
        }

        HashedPassword = userManager.HashPassword(newPassword);
    }
}