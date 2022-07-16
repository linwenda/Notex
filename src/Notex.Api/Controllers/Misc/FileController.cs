using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Infrastructure.FileProviders;

namespace Notex.Api.Controllers.Misc;

[Authorize]
[Route("files")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {
        return Ok(await _fileService.UploadAsync(file));
    }

    [HttpGet("{fileId:guid}")]
    public async Task<IActionResult> GetFileAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var appFile = await _fileService.GetAsync(fileId, cancellationToken);

        return new FileContentResult(appFile.Content, appFile.ContentType);
    }
}