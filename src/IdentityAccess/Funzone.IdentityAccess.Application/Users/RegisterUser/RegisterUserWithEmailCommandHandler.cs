using Funzone.BuildingBlocks.Application.Commands;
using Funzone.IdentityAccess.Application.Authentication;
using Funzone.IdentityAccess.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Application.Users.RegisterUser
{
    public class RegisterUserWithEmailCommandHandler : ICommandHandler<RegisterUserWithEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCounter _userCounter;

        public RegisterUserWithEmailCommandHandler(IUserRepository userRepository, IUserCounter userCounter)
        {
            _userRepository = userRepository;
            _userCounter = userCounter;
        }

        public async Task<Unit> Handle(RegisterUserWithEmailCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = PasswordManager.CreateSalt(5);
            var passwordHash = PasswordManager.HashPassword(request.Password, passwordSalt);

            var user = User.RegisterWithEmail(
                new EmailAddress(request.EmailAddress),
                passwordSalt,
                passwordHash,
                _userCounter);

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}