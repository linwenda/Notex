using System.Threading.Tasks;
using MediatR;
using NUnit.Framework;

namespace Funzone.Services.Albums.IntegrationTests
{
    using static TestFixture;

    public class TestBase
    {
        protected IMediator Mediator { get; private set; }

        [SetUp]
        public async Task SetUp()
        {
            await Cleanup();
        }
    }
}