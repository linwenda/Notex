using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Services.Albums.Api.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreateAlbumCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
