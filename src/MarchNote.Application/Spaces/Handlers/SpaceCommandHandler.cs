using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Spaces.Handlers
{
    public class SpaceCommandHandler :
        ICommandHandler<CreateSpaceCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<DeleteSpaceCommand, MarchNoteResponse>,
        ICommandHandler<RenameSpaceCommand, MarchNoteResponse>,
        ICommandHandler<AddFolderSpaceCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<MoveSpaceCommand, MarchNoteResponse>,
        ICommandHandler<UpdateSpaceBackgroundCommand, MarchNoteResponse>
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

        public async Task<MarchNoteResponse<Guid>> Handle(CreateSpaceCommand request,
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

            return new MarchNoteResponse<Guid>(space.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            space.SoftDelete(_userContext.UserId);

            await _spaceRepository.UpdateAsync(space);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(RenameSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            space.Rename(_userContext.UserId, request.Name);

            await _spaceRepository.UpdateAsync(space);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse<Guid>> Handle(AddFolderSpaceCommand request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            var folderSpace = space.AddFolder(_userContext.UserId, request.Name);

            await _spaceRepository.InsertAsync(folderSpace);

            return new MarchNoteResponse<Guid>(folderSpace.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(MoveSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            var destSpace = await _spaceRepository.FindAsync(new SpaceId(request.DestSpaceId));

            space.Move(_userContext.UserId, destSpace);

            await _spaceRepository.UpdateAsync(space);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(UpdateSpaceBackgroundCommand request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.FindAsync(new SpaceId(request.SpaceId));

            space.SetBackground(new Background("", request.BackgroundImageId));

            await _spaceRepository.UpdateAsync(space);

            return new MarchNoteResponse();
        }
    }
}