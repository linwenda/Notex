using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.IntegrationTests.Notes;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Commands;
using Respawn;
using Respawn.Graph;

namespace Notex.IntegrationTests;

internal class TestHelper
{
    private readonly IMediator _mediator;
    private readonly IRepository<SpaceDetail> _repository;
    private readonly IConfiguration _configuration;

    public TestHelper(
        IMediator mediator,
        IRepository<SpaceDetail> repository,
        IConfiguration configuration)
    {
        _mediator = mediator;
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<Guid> CreateDefaultSpaceAsync()
    {
        var command = new CreateSpaceCommand
        {
            Name = "Default",
            BackgroundImage = "https://img.microsoft.com",
            Visibility = Visibility.Public
        };

        var space = await _repository.Query().FirstOrDefaultAsync(s => s.Name == command.Name);

        if (space != null)
        {
            return space.Id;
        }

        return await _mediator.Send(command);
    }

    public async Task<Guid> CreateDefaultNoteAsync(NoteOptions options)
    {
        var spaceId = await CreateDefaultSpaceAsync();

        var command = new CreateNoteCommand
        {
            SpaceId = spaceId,
            Title = ".Net 6",
            Content = ".Net 6 new feature",
            Status = options.Status
        };

        return await _mediator.Send(command);
    }

    public async Task CleanDatabaseAsync()
    {
        var checkPoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                new Table("__EFMigrationsHistory")
            },
            DbAdapter = DbAdapter.MySql
        };

        await using var connectionString = new MySqlConnection(_configuration.GetConnectionString("Default"));
        connectionString.Open();
        await checkPoint.Reset(connectionString);
    }
}