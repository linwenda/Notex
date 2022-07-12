using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Notex.Core.DependencyInjection;
using Xunit;

namespace Notex.UnitTests.DependencyInjection;

public class DependencyInjectionTests
{
    private readonly IServiceCollection _serviceCollection;
    private readonly ServiceProvider _serviceProvider;

    public DependencyInjectionTests()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddRegistrationByConvention(Assembly.GetExecutingAssembly());
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void AddTransient_WithTransientLifetime_IsSuccessful()
    {
        var serviceDescriptor = _serviceCollection.Single(s => s.ServiceType == typeof(ITestTransientService));
        Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);

        var service = _serviceProvider.GetService<ITestTransientService>();
        Assert.NotNull(service);
    }

    [Fact]
    public void AddScope_WithScopeLifetime_IsSuccessful()
    {
        var serviceDescriptor = _serviceCollection.Single(s => s.ServiceType == typeof(ITestScopedService));
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);

        var scopedService = _serviceProvider.GetService<ITestScopedService>();
        Assert.NotNull(scopedService);
    }

    [Fact]
    public void AddSingleton_WithSingletonLifetime_IsSuccessful()
    {
        var serviceDescriptor = _serviceCollection.Single(s => s.ServiceType == typeof(ITestSingletonService));
        Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);

        var singletonService = _serviceProvider.GetService<ITestSingletonService>();
        Assert.NotNull(singletonService);
    }

    [Fact]
    public void AddSelf_IsSuccessful()
    {
        var selfService = _serviceProvider.GetService<TestSelfService>();
        Assert.NotNull(selfService);
    }

    [Fact]
    public void AddGeneric_IsSuccessful()
    {
        var genericService = _serviceProvider.GetService<ITestGenericService<Guid>>();
        Assert.NotNull(genericService);

        var genericService2 = _serviceProvider.GetService<ITestGenericService2<Guid, Guid>>();
        Assert.NotNull(genericService2);

        var genericService3 = _serviceProvider.GetService<ITestGenericService3>();
        Assert.NotNull(genericService3);
    }

    private interface ITestTransientService : ITransientLifetime
    {
    }

    private class TestTransientService : ITestTransientService
    {
    }

    private interface ITestScopedService : IScopedLifetime
    {
    }

    public class TestScopedService : ITestScopedService
    {
    }

    private interface ITestSingletonService : ISingletonLifetime
    {
    }

    private class TestSingletonService : ITestSingletonService
    {
    }

    private class TestSelfService : ITransientLifetime
    {
    }

    private interface ITestGenericService<T> : ITransientLifetime
    {
    }

    private class TestGenericService<T> : ITestGenericService<T>
    {
    }

    private interface ITestGenericService2<T1, T2> : ITransientLifetime
    {
    }

    private class TestGenericService2<T1, T2> : ITestGenericService2<T1, T2>
    {
    }

    private interface ITestGenericService3 : ITestGenericService2<int, int>
    {
    }

    private class TestGenericService3 : ITestGenericService3
    {
    }
}