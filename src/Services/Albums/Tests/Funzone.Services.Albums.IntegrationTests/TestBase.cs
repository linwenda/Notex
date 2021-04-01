using System.Threading.Tasks;
using NUnit.Framework;

namespace Funzone.Services.Albums.IntegrationTests
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