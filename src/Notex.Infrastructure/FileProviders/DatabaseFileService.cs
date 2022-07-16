using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Exceptions;
using Notex.Core.Identity;
using Notex.Infrastructure.Data;

namespace Notex.Infrastructure.FileProviders;

public class DatabaseFileService : IFileService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public DatabaseFileService(ApplicationDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<AppFile> GetAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var appFile = await _dbContext.AppFiles.FirstOrDefaultAsync(f => f.Id == fileId, cancellationToken);

        if (appFile == null)
        {
            throw new EntityNotFoundException(typeof(AppFile), fileId);
        }

        return appFile;
    }

    public async Task<Guid> UploadAsync(IFormFile file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        
        await using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);

        // Upload the file if less than 2 MB
        if (memoryStream.Length < 2097152)
        {
            var appFile = new AppFile
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                CreatorId = _currentUser.Id,
                Content = memoryStream.ToArray(),
                ContentType = file.ContentType,
                Name = file.FileName
            };

            _dbContext.AppFiles.Add(appFile);

            await _dbContext.SaveChangesAsync();

            return appFile.Id;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(file), "The file is too large.");
        }
    }
}