using NUnit.Framework;

namespace Funzone.Services.Albums.IntegrationTests
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }
        
        [SetUp]
        public void Setup()
        {
           
        }
    }
}