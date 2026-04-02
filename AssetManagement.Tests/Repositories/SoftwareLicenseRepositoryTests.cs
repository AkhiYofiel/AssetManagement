using AssetManagementApi.Repositories;
using Xunit;

namespace AssetManagement.Tests.Repositories;

public class SoftwareLicenseRepositoryTests
{
    [Fact]
    public async Task GetDetailAsync_WhenMissing_ReturnsNull()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var repository = new SoftwareLicenseRepository(context);

        var license = await repository.GetDetailAsync(999);

        Assert.Null(license);
    }
}
