using System;
using System.IO;
using System.Threading.Tasks;
using MarchNote.Application.Attachments;
using Microsoft.AspNetCore.Http;

namespace MarchNote.Infrastructure.Attachments
{
    public class LocalAttachmentServer : IAttachmentServer
    {
        private readonly string _savePath;

        public LocalAttachmentServer(string savePath)
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

            using (var stream = File.Create($"{saveFolder}\\{file.FileName}"))
            {
                await file.CopyToAsync(stream);
            }

            return UploadResult.Success($"{saveFolder}\\{file.FileName}", file.FileName);
        }
    }
}