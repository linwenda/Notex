using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Commands.Users.Authenticate;
using Funzone.Application.Contract;
using Funzone.Domain.Users;
using MediatR;

namespace Funzone.Application.Commands.Users.RegisterUser
{
    public class RegisterUserByEmailCommandHandler : ICommandHandler<RegisterUserByEmailCommand>
    {
        private readonly IUserChecker _userChecker;
        private readonly IUserRepository _userRepository;

        public RegisterUserByEmailCommandHandler(
            IUserChecker userChecker,
            IUserRepository userRepository)
        {
            _userChecker = userChecker;
            _userRepository = userRepository;
        }
        
        public async Task<Unit> Handle(RegisterUserByEmailCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = PasswordManager.CreateSalt(5);
            var passwordHash = PasswordManager.HashPassword(request.Password, passwordSalt);

            var user = User.RegisterByEmail(
                _userChecker,
                new EmailAddress(request.EmailAddress),
                passwordSalt,
                passwordHash);

            await _userRepository.AddAsync(user);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}