using Autofac;
using Autofac.Extensions.DependencyInjection;
using Funzone.Api.Configuration;
using Funzone.BuildingBlocks.Application;
using Funzone.IdentityAccess.Infrastructure;
using Funzone.PhotoAlbums.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funzone.Api", Version = "v1" });
            });

            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Initialize(container);
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {

        }

        private void Initialize(ILifetimeScope container)
        {
            var connectionString1 = "localhost;uid=root;pwd=123123123;database=funzone;";
            var connectionString = "Server=localhost;Database=funzone;Uid=root;Pwd=123123123;";

            IdentityAccessStartup.Initialize(
                connectionString,
                Log.Logger);

            PhotoAlbumsStartup.Initialize(
                connectionString,
                Log.Logger);
        }
    }
}
