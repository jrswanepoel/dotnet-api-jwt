using Xunit;

namespace Starter.API.IntegrationTests.Controllers.Resources
{
    [CollectionDefinition("IntegrationTestFixture")]
    public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture>
    {
    }
}
