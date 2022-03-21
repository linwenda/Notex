using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Spaces.ReadModels;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Infrastructure;
using Notex.Infrastructure.EntityFrameworkCore;
using Notex.IntegrationTests.Mock;
using Notex.IntegrationTests.Notes;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Commands;
using Npgsql;
using Respawn;
using Respawn.Graph;

namespace Notex.IntegrationTests;

public class IntegrationFixture : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public IntegrationFixture()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfrastructure(_configuration);
        serviceCollection.AddTransient<ICurrentUser, FakeCurrentUser>();
        serviceCollection.AddTransient<IResourceAuthorizationService, FakeResourceAuthorizationService>();
        serviceCollection.AddSingleton(_configuration);
        serviceCollection.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

        _serviceProvider = serviceCollection.BuildServiceProvider();

        InitDatabase();
    }

    public void Dispose()
    {
        CleanDatabase();
    }

    public T GetService<T>() => _serviceProvider.GetRequiredService<T>();
    public IMediator Mediator => GetService<IMediator>();

    public async Task<Guid> GetOrCreateDefaultSpaceAsync()
    {
        var command = new CreateSpaceCommand
        {
            Name = "Default",
            BackgroundImage = "https://img.microsoft.com",
            Visibility = Visibility.Public
        };

        var space = await GetService<IReadModelRepository>().Query<SpaceDetail>()
            .FirstOrDefaultAsync(s => s.Name == command.Name);

        if (space != null)
        {
            return space.Id;
        }

        return await GetService<IMediator>().Send(command);
    }

    public async Task<Guid> CreateDefaultNoteAsync(NoteOptions options)
    {
        var spaceId = await GetOrCreateDefaultSpaceAsync();

        var command = new CreateNoteCommand
        {
            SpaceId = spaceId,
            Title = ".Net 6",
            Content = ".Net 6 new feature",
            Status = options.Status
        };

        return await GetService<IMediator>().Send(command);
    }

    private void InitDatabase()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<NotexDbContext>();
            if (dbContext.Database.IsNpgsql())
            {
                dbContext.Database.Migrate();
            }
        }
    }

    private void CleanDatabase()
    {
        var checkPoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                new Table("__EFMigrationsHistory")
            },
            SchemasToInclude = new[]
            {
                "public"
            },
            DbAdapter = DbAdapter.Postgres
        };

        var connectionString = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
        
        using (var conn = connectionString)
        {
            conn.Open();

            checkPoint.Reset(conn).GetAwaiter().GetResult();
        }
    }
}