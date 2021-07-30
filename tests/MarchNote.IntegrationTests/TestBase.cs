using NUnit.Framework;

namespace MarchNote.IntegrationTests
{
    using static TestFixture;

    public class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Cleanup();
        }
    }
}