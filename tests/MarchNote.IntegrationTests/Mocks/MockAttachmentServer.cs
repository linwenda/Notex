using System.Threading.Tasks;
using MarchNote.Application.Attachments;
using Microsoft.AspNetCore.Http;

namespace MarchNote.IntegrationTests.Mocks
{
    public class MockAttachmentServer : IAttachmentServer
    {
        public Task<UploadResult> UploadAsync(IFormFile file)
        {
            return Task.FromResult(UploadResult.Success("test", file.FileName));
        }
    }
}