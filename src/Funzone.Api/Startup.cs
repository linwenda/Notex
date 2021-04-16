using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Funzone.Api.Configuration;
using Funzone.Api.Configuration.Filters;
using Funzone.Api.Configuration.Identity;
using Funzone.Application.Configuration;
using Funzone.Infrastructure;
using IdentityServer4.Validation;
using Serilog;

namespace Funzone.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });

            services.AddHttpContextAccessor();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddSingleton(Log.Logger);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Funzone.Api", Version = "v1"});
            });
            
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();
            
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            return InitializeServiceProvider(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.Api v1"));
            }

            app.UseRouting();

            app.UseIdentityServer();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private IServiceProvider InitializeServiceProvider(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SqlServer");
            var serviceProvider = services.BuildServiceProvider();

            return FunzoneStartup.Initialize(
                services,
                connectionString,
                serviceProvider.GetRequiredService<IExecutionContextAccessor>(),
                serviceProvider.GetRequiredService<ILogger>());
        }
    }
}