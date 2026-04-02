using AssetManagementApi.Data;
using AssetManagementApi.Models;
using AssetManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AssetManagement.Tests.Repositories;

public class RepositoryTests
{
    private static AssetManagementContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AssetManagementContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AssetManagementContext(options);
    }

    [Fact]
    public async Task StatusRepository_CreateAndGetById_ReturnsData()
    {
        await using var context = CreateContext();
        var repository = new StatusRepository(context);

        var status = new Status { Name = "Active" };

        await repository.CreateAsync(status);
        await repository.SaveChangesAsync();

        var status = await repository.GetByIdAsync(status.Id);

        Assert.NotNull(fromDb);
        Assert.Equal("Active", status!.Name);
    }

    [Fact]
    public async Task AssetRepository_GetDetailAsync_ReturnsData()
    {
        await using var context = CreateContext();

        var status = new Status { Id = 1, Name = "Assigned" };
        var employee = new Employee { Id = 2, FirstName = "Martin", LastName = "M", Email = "example@example.com" };
        var license = new SoftwareLicense { Id = 3, Name = "MSOffice", LicenseKey = "ABC123", ExpirationDate = DateTime.UtcNow.AddDays(30) };
        var asset = new Asset { Id = 4, Name = "Laptop", SerialNumber = "SN001", StatusId = status.Id, EmployeeId = employee.Id };
        var warranty = new WarrantyCard { Id = 5, Provider = "Dell", StartDate = DateTime.UtcNow.Date, EndDate = DateTime.UtcNow.Date.AddYears(1), AssetId = asset.Id };
        var link = new AssetSoftwareLicense { AssetId = asset.Id, SoftwareLicenseId = license.Id };

        context.Statuses.Add(status);
        context.Employees.Add(employee);
        context.SoftwareLicenses.Add(license);
        context.Assets.Add(asset);
        context.WarrantyCards.Add(warranty);
        context.AssetSoftwareLicenses.Add(link);
        await context.SaveChangesAsync();

        var repository = new AssetRepository(context);
        var detail = await repository.GetDetailAsync(asset.Id);

        Assert.NotNull(detail);
        Assert.NotNull(detail!.Status);
        Assert.NotNull(detail.Employee);
        Assert.NotNull(detail.WarrantyCard);
        Assert.Single(detail.AssetSoftwareLicenses);
    }

    [Fact]
    public async Task StatusRepository_GetAllAsync_ReturnsAllStatuses()
    {
        await using var context = CreateContext();
        var repository = new StatusRepository(context);

        await repository.CreateAsync(new Status { Name = "In Stock" });
        await repository.CreateAsync(new Status { Name = "Assigned" });
        await repository.SaveChangesAsync();

        var all = await repository.GetAllAsync();

        Assert.Equal(2, all.Count());
    }

    [Fact]
    public async Task StatusRepository_DeleteAsync_WhenIdNotFound_DoesNotThrow()
    {
        await using var context = CreateContext();
        var repository = new StatusRepository(context);

        var exception = await Record.ExceptionAsync(() => repository.DeleteAsync(999));

        Assert.Null(exception);
    }

    [Fact]
    public async Task AssetRepository_StatusExistsAsync_ReturnsExpectedResult()
    {
        await using var context = CreateContext();
        context.Statuses.Add(new Status { Id = 1, Name = "Active" });
        await context.SaveChangesAsync();

        var repository = new AssetRepository(context);

        var exists = await repository.StatusExistsAsync(1);
        var missing = await repository.StatusExistsAsync(999);

        Assert.True(exists);
        Assert.False(missing);
    }

    [Fact]
    public async Task EmployeeRepository_GetDetailAsync_WhenMissing_ReturnsNull()
    {
        await using var context = CreateContext();
        var repository = new EmployeeRepository(context);

        var employee = await repository.GetDetailAsync(999);

        Assert.Null(employee);
    }

    [Fact]
    public async Task SoftwareLicenseRepository_GetDetailAsync_WhenMissing_ReturnsNull()
    {
        await using var context = CreateContext();
        var repository = new SoftwareLicenseRepository(context);

        var license = await repository.GetDetailAsync(999);

        Assert.Null(license);
    }
}
