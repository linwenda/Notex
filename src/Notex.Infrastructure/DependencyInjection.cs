using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notex.Core;
using Notex.Core.Configuration;
using Notex.Core.Lifetimes;
using Notex.Infrastructure.EntityFrameworkCore;
using Notex.Infrastructure.Mediation;

namespace Notex.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var thisAssembly = typeof(DependencyInjection).Assembly;

            serviceCollection.AddCore();

            serviceCollection.AddRegistrationByConvention(thisAssembly);

            serviceCollection.AddDbContext<NotexDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"))
                    .UseSnakeCaseNamingConvention());

            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        }
    }
}