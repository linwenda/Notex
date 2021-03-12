using Autofac;
using Funzone.BuildingBlocks.EventBusDapr;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Funzone.IdentityAccess.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceCollection ServiceCollection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IEventBus, DaprEventBus>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funzone.IdentityAccess.Api", Version = "v1" });
            });

            ServiceCollection = services;
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var connectionString = Configuration.GetConnectionString("MySql");//TODO:docker-compose setting
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            containerBuilder.RegisterModule(new IdentityAccessModule(
                connectionString, 
                Log.Logger, 
                eventBus));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.IdentityAccess.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
