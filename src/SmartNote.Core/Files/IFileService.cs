using Microsoft.AspNetCore.Http;

namespace SmartNote.Core.Files;

public interface IFileService
{
    Task<UploadResult> UploadAsync(IFormFile file);
}