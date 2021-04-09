using System;
using System.Collections.Generic;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.ChangeVisibility;
using Funzone.Services.Albums.Application.Queries.GetAlbum;
using Funzone.Services.Albums.Application.Queries.GetUserAlbums;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        [ProducesResponseType(typeof(List<UserAlbumDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAlbums()
        {
            var result = await _mediator.Send(new GetUserAlbumsQuery());
            return Ok(result);
        }

        [HttpGet("{albumId}")]
        [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlbumById(Guid albumId)
        {
            var result = await _mediator.Send(new GetAlbumByIdQuery(albumId));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAlbum(CreateAlbumCommand command)
        {
            await _mediator.Send(command);
            return Created("", null);
        }

        [HttpPost("{albumId}/private")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MakePrivate([FromRoute] Guid albumId)
        {
            await _mediator.Send(new ChangeVisibilityCommand(albumId, Visibility.Private.Value));
            return NoContent();
        }

        [HttpPost("{albumId}/public")]
        [ProducesResponseType(StatusCodes.Status100Continue)]
        public async Task<IActionResult> MakePublic([FromRoute] Guid albumId)
        {
            await _mediator.Send(new ChangeVisibilityCommand(albumId, Visibility.Public.Value));
            return NoContent();
        }
    }
}