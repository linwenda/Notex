using Microsoft.EntityFrameworkCore;
using Notex.Api.Filters;
using Notex.Api.Identity;
using Notex.Api.Swagger;
using Notex.Core.Configuration;
using Notex.Infrastructure;
using Notex.Infrastructure.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomIdentityServer(builder.Configuration, builder.Environment);
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomSwagger();

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

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

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Seeding Database...");

await SeedDatabase(app);

app.Logger.LogInformation("Notex.Api Launching");

app.Run();


public partial class Program
{
    private static IConfiguration Configuration => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables()
        .Build();

    private static async Task SeedDatabase(IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var dbContext = serviceProvider.GetRequiredService<NotexDbContext>();

                if (dbContext.Database.IsNpgsql())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}