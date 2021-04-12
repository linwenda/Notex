using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.PictureComments;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.EditPictureComment
{
    public class EditPictureCommentCommandHandler : ICommandHandler<EditPictureCommentCommand>
    {
        private readonly IPictureCommentRepository _pictureCommentRepository;
        private readonly IUserContext _userContext;

        public EditPictureCommentCommandHandler(IPictureCommentRepository pictureCommentRepository,
            IUserContext userContext)
        {
            _pictureCommentRepository = pictureCommentRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditPictureCommentCommand request, CancellationToken cancellationToken)
        {
            var pictureComment = await _pictureCommentRepository.GetByIdAsync(
                new PictureCommentId(request.PictureCommentId));

            if (pictureComment == null) throw new NotFoundException(nameof(PictureComment), request.PictureCommentId);

            pictureComment.Edit(_userContext.UserId, request.Comment);

            return Unit.Value;
        }
    }
}