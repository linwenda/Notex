using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Pictures;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.AddPictureComment
{
    public class AddPictureCommentCommandHandler : ICommandHandler<AddPictureCommentCommand>
    {
        private readonly IPictureRepository _pictureRepository;

        public AddPictureCommentCommandHandler(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        
        public Task<Unit> Handle(AddPictureCommentCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}