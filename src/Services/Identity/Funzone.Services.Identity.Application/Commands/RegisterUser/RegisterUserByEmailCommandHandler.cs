using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Identity.Application.Commands.Authenticate;
using Funzone.Services.Identity.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.Services.Identity.Application.Commands.RegisterUser
{
    public class RegisterUserByEmailCommandHandler : ICommandHandler<RegisterUserByEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCounter _userCounter;

        public RegisterUserByEmailCommandHandler(
            IUserRepository userRepository,
            IUserCounter userCounter)
        {
            _userRepository = userRepository;
            _userCounter = userCounter;
        }

        public async Task<Unit> Handle(RegisterUserByEmailCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = PasswordManager.CreateSalt(5);
            var passwordHash = PasswordManager.HashPassword(request.Password, passwordSalt);

            var user = User.RegisterByEmail(
                _userCounter,
                new EmailAddress(request.EmailAddress),
                passwordSalt,
                passwordHash);

            await _userRepository.AddAsync(user);
            return Unit.Value;
        }
    }
}