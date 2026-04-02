using AssetManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Tests.Repositories;

internal static class TestDbContextFactory
{
    public static AssetManagementContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AssetManagementContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AssetManagementContext(options);
    }
}
