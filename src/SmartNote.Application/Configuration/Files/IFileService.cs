using Microsoft.AspNetCore.Http;

namespace SmartNote.Application.Configuration.Files;

public interface IFileService
{
    Task<UploadResult> UploadAsync(IFormFile file);
}