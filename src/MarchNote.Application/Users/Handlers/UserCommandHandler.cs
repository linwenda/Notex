using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Users.Command;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
using MarchNote.Domain.Users.Exceptions;
using MediatR;

namespace MarchNote.Application.Users.Handlers
{
    public class UserCommandHandler :
        ICommandHandler<AuthenticateCommand, UserAuthenticateDto>,
        ICommandHandler<RegisterUserCommand, Guid>,
        ICommandHandler<ChangePasswordCommand, Unit>,
        ICommandHandler<UpdateProfileCommand, Unit>
    {
        private readonly IUserChecker _userChecker;
        private readonly IUserContext _userContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<User> _userRepository;

        public UserCommandHandler(
            IUserChecker userChecker,
            IUserContext userContext,
            IEncryptionService encryptionService,
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _userChecker = userChecker;
            _userContext = userContext;
        }

        public async Task<UserAuthenticateDto> Handle(AuthenticateCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) throw new IncorrectEmailOrPasswordException();

            user.CheckPassword(_encryptionService, request.Password);

            return new UserAuthenticateDto
            {
                Id = user.Id,
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                },
                Email = user.Email,
                IsActive = user.IsActive,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<Guid> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = User.Register(
                _userChecker,
                _encryptionService,
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName);

            await _userRepository.InsertAsync(user);

            return user.Id;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CheckNotNull(_userContext.UserId);

            user.ChangePassword(_encryptionService, request.OldPassword, request.NewPassword);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CheckNotNull(_userContext.UserId);

            user.UpdateProfile(
                request.FirstName,
                request.LastName,
                request.Bio,
                request.Avatar);

            await _userRepository.UpdateAsync(user);
            
            return Unit.Value;
        }
    }
}