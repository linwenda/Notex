using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.Spaces.Handlers
{
    public class SpaceCommandHandler :
        ICommandHandler<CreateSpaceCommand,Guid>,
        ICommandHandler<DeleteSpaceCommand, Unit>,
        ICommandHandler<RenameSpaceCommand, Unit>,
        ICommandHandler<AddFolderSpaceCommand,Guid>,
        ICommandHandler<UpdateSpaceBackgroundCommand, Unit>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<Space> _spaceRepository;
        private readonly ISpaceChecker _spaceChecker;

        public SpaceCommandHandler(
            IUserContext userContext,
            IRepository<Space> spaceRepository,
            ISpaceChecker spaceChecker)
        {
            _userContext = userContext;
            _spaceRepository = spaceRepository;
            _spaceChecker = spaceChecker;
        }

        public async Task<Guid> Handle(CreateSpaceCommand request,
            CancellationToken cancellationToken)
        {
            var space = await Space.Create(
                _spaceChecker,
                _userContext.UserId,
                request.Name,
                new Background(request.BackgroundColor, request.BackgroundImageId),
                request.Visibility,
                request.Description);

            await _spaceRepository.InsertAsync(space);

            return space.Id;
        }

        public async Task<Unit> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.CheckNotNull(request.SpaceId);

            space.SoftDelete(_userContext.UserId);

            await _spaceRepository.UpdateAsync(space);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RenameSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.CheckNotNull(request.SpaceId);

            space.Rename(_userContext.UserId, request.Name);

            await _spaceRepository.UpdateAsync(space);

            return Unit.Value;
        }

        public async Task<Guid> Handle(AddFolderSpaceCommand request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.CheckNotNull(request.SpaceId);

            var folderSpace = space.AddFolder(_userContext.UserId, request.Name);

            await _spaceRepository.InsertAsync(folderSpace);

            return folderSpace.Id;
        }

        public async Task<Unit> Handle(UpdateSpaceBackgroundCommand request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.CheckNotNull(request.SpaceId);

            space.SetBackground(new Background("", request.BackgroundImageId));

            await _spaceRepository.UpdateAsync(space);

            return Unit.Value;
        }
    }
}