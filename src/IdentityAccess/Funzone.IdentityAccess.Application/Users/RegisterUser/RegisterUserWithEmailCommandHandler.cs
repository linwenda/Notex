using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Application.Authentication;
using Funzone.IdentityAccess.Application.IntegrationEvents.Events;
using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Domain.Users.Events;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Application.Users.RegisterUser
{
    public class RegisterUserWithEmailCommandHandler : ICommandHandler<RegisterUserWithEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCounter _userCounter;
        private readonly IEventBus _eventBus;

        public RegisterUserWithEmailCommandHandler(
            IUserRepository userRepository,
            IUserCounter userCounter,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _userCounter = userCounter;
            _eventBus = eventBus;
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