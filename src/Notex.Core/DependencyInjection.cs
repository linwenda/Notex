using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notex.Core.Configuration;
using Notex.Core.Lifetimes;

namespace Notex.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
    {
        var thisAssembly = typeof(DependencyInjection).Assembly;

        serviceCollection.AddRegistrationByConvention(thisAssembly);
        serviceCollection.AddAutoMapper(thisAssembly);
        serviceCollection.AddValidatorsFromAssembly(thisAssembly);
        serviceCollection.AddMediatR(thisAssembly);
        
        return serviceCollection;
    }
}