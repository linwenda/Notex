using Funzone.PhotoAlbums.Application.Commands.CreateAlbum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Api.Albums
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
            return Ok();
        }
    }
}
