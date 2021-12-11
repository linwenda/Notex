using MediatR;
using SmartNote.Core.Application.Spaces.Contracts;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Spaces;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Spaces.Handlers
{
    public class SpaceCommandHandler :
        ICommandHandler<CreateSpaceCommand, Guid>,
        ICommandHandler<DeleteSpaceCommand, Unit>,
        ICommandHandler<AddFolderSpaceCommand, Guid>,
        ICommandHandler<UpdateSpaceCommand, Unit>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Space> _spaceRepository;
        private readonly ISpaceChecker _spaceChecker;

        public SpaceCommandHandler(
            ICurrentUser currentUser,
            IRepository<Space> spaceRepository,
            ISpaceChecker spaceChecker)
        {
            _currentUser = currentUser;
            _spaceRepository = spaceRepository;
            _spaceChecker = spaceChecker;
        }

        public async Task<Guid> Handle(CreateSpaceCommand request,
            CancellationToken cancellationToken)
        {
            var space = await Space.Create(
                _spaceChecker,
                _currentUser.Id,
                request.Name,
                new Background(request.BackgroundColor, request.BackgroundImageId),
                request.Visibility);

            await _spaceRepository.InsertAsync(space);

            return space.Id;
        }

        public async Task<Unit> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.GetAsync(request.SpaceId);

            space.SoftDelete(_currentUser.Id);

            await _spaceRepository.UpdateAsync(space);

            return Unit.Value;
        }

        public async Task<Guid> Handle(AddFolderSpaceCommand request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.GetAsync(request.SpaceId);

            var folderSpace = space.AddFolder(_currentUser.Id, request.Name);

            await _spaceRepository.InsertAsync(folderSpace);

            return folderSpace.Id;
        }

        public async Task<Unit> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.GetAsync(request.SpaceId);

            await space.Update(
                _spaceChecker,
                request.Name,
                request.Visibility,
                request.BackgroundImageId);

            await _spaceRepository.UpdateAsync(space);

            return Unit.Value;
        }
    }
}