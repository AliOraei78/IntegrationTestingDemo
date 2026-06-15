using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
}