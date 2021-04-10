using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Pictures;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.DeletePicture
{
    public class DeletePictureCommandHandler : ICommandHandler<DeletePictureCommand>
    {
        private readonly IPictureRepository _pictureRepository;

        public DeletePictureCommandHandler(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public Task<Unit> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}