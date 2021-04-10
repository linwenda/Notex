using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.DeletePicture
{
    public class DeletePictureCommandHandler : ICommandHandler<DeletePictureCommand>
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IUserContext _userContext;

        public DeletePictureCommandHandler(
            IPictureRepository pictureRepository,
            IUserContext userContext)
        {
            _pictureRepository = pictureRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
        {
            var picture = await _pictureRepository.GetByIdAsync(new PictureId(request.PictureId));
            if (picture == null) throw new NotFoundException(nameof(Picture), request.PictureId);

            picture.CheckHandler(_userContext.UserId);

            _pictureRepository.Delete(picture);

            return Unit.Value;
        }
    }
}