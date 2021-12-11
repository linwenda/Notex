using Autofac;
using Autofac.Extensions.DependencyInjection;
using SmartNote.Api.Configuration;
using SmartNote.Api.Configuration.Identity;
using SmartNote.Api.Configuration.Swagger;
using SmartNote.Api.Filters;
using SmartNote.Core.Application;
using SmartNote.Core.Security;
using SmartNote.Infrastructure;
using ILogger = Serilog.ILogger;

namespace SmartNote.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
        services.AddEndpointsApiExplorer();
        services.AddCustomSwagger();
        services.AddHttpContextAccessor();
        services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
        services.AddSingleton<ILogger>(Log.Logger);
        services.AddCustomIdentityServer(Configuration);

        var container = new ContainerBuilder();

        container.Populate(services);
        container.RegisterModule(new SmartNoteModule(
            Configuration.GetConnectionString("SqlServer"),
            Configuration.GetValue<string>("FileService:StorePath"),
            GetService<ILogger>(services),
            GetService<IExecutionContextAccessor>(services),
            typeof(SmartNoteModule).Assembly,
            typeof(ICommandHandler<,>).Assembly));

        return new AutofacServiceProvider(container.Build());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static T GetService<T>(IServiceCollection serviceCollection)
    {
        return serviceCollection.BuildServiceProvider().GetService<T>() ??
               throw new NullReferenceException(typeof(T).Name);
    }
}