using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartNote.Core.Files;

namespace SmartNote.IntegrationTests.Mocks;

public class MockFileService : IFileService
{
    public Task<UploadResult> UploadAsync(IFormFile file)
    {
        return Task.FromResult(UploadResult.Success("test", file.FileName));
    }
}