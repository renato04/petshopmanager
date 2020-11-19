using System;
using System.Net.Http;
using Xunit;

namespace PetShopIntegrationTests
{
    public class UnitTest1 : IClassFixture<CustomWebApplicationFactory>
    {
        private HttpClient _client;

        public UnitTest1(CustomWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
