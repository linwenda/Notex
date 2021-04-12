using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.PictureComments;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.DeletePictureComment
{
    public class DeletePictureCommentCommandHandler : ICommandHandler<DeletePictureCommentCommand>
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IPictureCommentRepository _pictureCommentRepository;
        private readonly IUserContext _userContext;

        public DeletePictureCommentCommandHandler(
            IPictureRepository pictureRepository,
            IPictureCommentRepository pictureCommentRepository,
            IUserContext userContext)
        {
            _pictureRepository = pictureRepository;
            _pictureCommentRepository = pictureCommentRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeletePictureCommentCommand request, CancellationToken cancellationToken)
        {
            var pictureComment = await _pictureCommentRepository.GetByIdAsync(
                new PictureCommentId(request.PictureCommentId));

            if (pictureComment == null) throw new NotFoundException(nameof(PictureComment), request.PictureCommentId);

            var picture = await _pictureRepository.GetByIdAsync(pictureComment.PictureId);

            pictureComment.CheckCanDelete(_userContext.UserId, picture.AuthorId);

            _pictureCommentRepository.Delete(pictureComment);

            return Unit.Value;
        }
    }
}