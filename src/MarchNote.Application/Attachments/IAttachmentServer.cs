using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MarchNote.Application.Attachments
{
    public interface IAttachmentServer
    {
        Task<UploadResult> UploadAsync(IFormFile file);
    }
}