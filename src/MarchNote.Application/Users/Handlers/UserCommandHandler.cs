using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Command;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users.Handlers
{
    public class UserCommandHandler :
        ICommandHandler<AuthenticateCommand, MarchNoteResponse<UserAuthenticateDto>>,
        ICommandHandler<RegisterUserCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<ChangePasswordCommand, MarchNoteResponse>,
        ICommandHandler<UpdateProfileCommand, MarchNoteResponse>
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

        public async Task<MarchNoteResponse<UserAuthenticateDto>> Handle(AuthenticateCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) throw new NotFoundException("Incorrect email address or password");

            user.CheckPassword(_encryptionService, request.Password);

            return new MarchNoteResponse<UserAuthenticateDto>(new UserAuthenticateDto
            {
                Id = user.Id.Value,
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                },
                Email = user.Email,
                IsActive = user.IsActive,
            });
        }

        public async Task<MarchNoteResponse<Guid>> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = User.Register(
                _userChecker,
                _encryptionService,
                request.Email,
                request.Password);

            await _userRepository.InsertAsync(user);

            return new MarchNoteResponse<Guid>(user.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userContext.UserId);

            user.ChangePassword(_encryptionService, request.OldPassword, request.NewPassword);

            await _userRepository.UpdateAsync(user);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userContext.UserId);

            user.UpdateProfile(_userChecker, request.NickName, request.Bio, request.Avatar);

            await _userRepository.UpdateAsync(user);

            return new MarchNoteResponse();
        }
    }
}