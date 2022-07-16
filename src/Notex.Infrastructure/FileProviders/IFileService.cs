using Microsoft.AspNetCore.Http;
using Notex.Core.DependencyInjection;

namespace Notex.Infrastructure.FileProviders;

public interface IFileService : IScopedLifetime
{
    Task<AppFile> GetAsync(Guid fileId, CancellationToken cancellationToken);
    Task<Guid> UploadAsync(IFormFile file);
}