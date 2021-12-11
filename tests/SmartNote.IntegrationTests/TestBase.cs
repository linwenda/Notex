using NUnit.Framework;

namespace SmartNote.IntegrationTests
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