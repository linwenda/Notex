using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Notex.Api.Authentication;
using Notex.Api.Filters;
using Notex.Api.Swagger;
using Notex.Core.Identity;
using Notex.Infrastructure;
using Notex.Infrastructure.Data;
using Notex.Infrastructure.EventSourcing;
using Notex.Infrastructure.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>()).AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((ctx, cfg) =>
{
    var seqServerUrl = ctx.Configuration["Serilog:SeqServerUrl"];

    cfg.Enrich.FromLogContext()
        .Enrich.WithThreadId()
        .Enrich.WithProperty("ApplicationContext", typeof(Program).Namespace)
        .Enrich.WithCorrelationIdHeader()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrEmpty(seqServerUrl) ? "http://seq" : seqServerUrl)
        .ReadFrom.Configuration(ctx.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b =>
{
    b.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Seeding Database...");

await SeedDatabaseAsync();

app.Logger.LogInformation("Notex.Api Launching");

app.Run();

async Task SeedDatabaseAsync()
{
    using var scope = app.Services.CreateScope();

    var serviceProvider = scope.ServiceProvider;

    try
    {
        var eventSourcingDbContext = serviceProvider.GetRequiredService<EventSourcingDbContext>();

        if (eventSourcingDbContext.Database.IsMySql())
        {
            await eventSourcingDbContext.Database.MigrateAsync();
        }

        var identityAccessDbContext = serviceProvider.GetRequiredService<IdentityAccessDbContext>();

        if (identityAccessDbContext.Database.IsMySql())
        {
            await identityAccessDbContext.Database.MigrateAsync();
        }

        var creationReadDbContext = serviceProvider.GetRequiredService<ReadModelDbContext>();

        if (creationReadDbContext.Database.IsMySql())
        {
            await creationReadDbContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.Message);
        throw;
    }
}