using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Commands;
using MarchNote.Application.Attachments.Queries;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Attachments
{
    using static TestFixture;

    public class AttachmentTests : TestBase
    {
        [Test]
        public async Task ShouldAddAttachment()
        {
            var addResponse = await Send(new AddAttachmentCommand(new MockFormFile()));

            var getResponse = await Send(new GetAttachmentQuery(addResponse));
            getResponse.Path.ShouldBe("test");
            getResponse.ContentType.ShouldBe("image/jpeg");
        }

        private class MockFormFile : IFormFile
        {
            public MockFormFile()
            {
                ContentType = "image/jpeg";
                ContentDisposition = "test";
                Headers = new HeaderDictionary();
                Length = 1024 * 1024 * 1;
                Name = "file";
                FileName = "test.jpg";
            }

            public Stream OpenReadStream()
            {
                throw new System.NotImplementedException();
            }

            public void CopyTo(Stream target)
            {
                throw new System.NotImplementedException();
            }

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new System.NotImplementedException();
            }

            public string ContentType { get; }
            public string ContentDisposition { get; }
            public IHeaderDictionary Headers { get; }
            public long Length { get; }
            public string Name { get; }
            public string FileName { get; }
        }
    }
}