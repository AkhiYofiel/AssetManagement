using AssetManagementApi.Models;
using AssetManagementApi.Repositories;
using Xunit;

namespace AssetManagement.Tests.Repositories;

public class StatusRepositoryTests
{
    [Fact]
    public async Task CreateAndGetById_ReturnsData()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var repository = new StatusRepository(context);

        var status = new Status { Name = "Active" };

        await repository.CreateAsync(status);
        await repository.SaveChangesAsync();

        var fromDb = await repository.GetByIdAsync(status.Id);

        Assert.NotNull(fromDb);
        Assert.Equal("Active", fromDb!.Name);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllStatuses()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var repository = new StatusRepository(context);

        await repository.CreateAsync(new Status { Name = "In Stock" });
        await repository.CreateAsync(new Status { Name = "Assigned" });
        await repository.SaveChangesAsync();

        var all = await repository.GetAllAsync();

        Assert.Equal(2, all.Count());
    }

    [Fact]
    public async Task DeleteAsync_WhenIdNotFound_DoesNotThrow()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var repository = new StatusRepository(context);

        var exception = await Record.ExceptionAsync(() => repository.DeleteAsync(999));

        Assert.Null(exception);
    }
}
