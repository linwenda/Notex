using System;
using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests
{
    using static TestFixture;
    
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await Cleanup();
        }
    }
}