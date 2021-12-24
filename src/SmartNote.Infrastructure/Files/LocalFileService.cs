using Microsoft.AspNetCore.Http;
using SmartNote.Application.Configuration.Files;

namespace SmartNote.Infrastructure.Files;

public class LocalFileService : IFileService
{
    private readonly string _savePath;

    public LocalFileService(string savePath)
    {
        _savePath = savePath;
    }

    public async Task<UploadResult> UploadAsync(IFormFile file)
    {
        if (file.Length <= 0)
        {
            return UploadResult.Failure("Empty file");
        }

        var saveFolder = $"{_savePath}\\{DateTime.UtcNow.Date:yyyyMMdd}";

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }

        var storedName = $"{Guid.NewGuid():N}-{file.FileName}";

        using (var stream = File.Create($"{saveFolder}\\{storedName}"))
        {
            await file.CopyToAsync(stream);
        }

        return UploadResult.Success($"{saveFolder}\\{storedName}", storedName);
    }
}