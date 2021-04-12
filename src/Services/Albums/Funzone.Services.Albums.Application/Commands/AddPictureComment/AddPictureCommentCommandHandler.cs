using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.AddPictureComment
{
    public class AddPictureCommentCommandHandler : ICommandHandler<AddPictureCommentCommand>
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IUserContext _userContext;

        public AddPictureCommentCommandHandler(IPictureRepository pictureRepository, IUserContext userContext)
        {
            _pictureRepository = pictureRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(AddPictureCommentCommand request, CancellationToken cancellationToken)
        {
            var picture = await _pictureRepository.GetByIdAsync(new PictureId(request.Id));
            picture.AddComment(_userContext.UserId, request.Comment);

            return Unit.Value;
        }
    }
}