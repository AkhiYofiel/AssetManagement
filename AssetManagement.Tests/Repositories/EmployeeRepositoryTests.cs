using AssetManagementApi.Repositories;
using Xunit;

namespace AssetManagement.Tests.Repositories;

public class EmployeeRepositoryTests
{
    [Fact]
    public async Task GetDetailAsync_WhenMissing_ReturnsNull()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var repository = new EmployeeRepository(context);

        var employee = await repository.GetDetailAsync(999);

        Assert.Null(employee);
    }
}
