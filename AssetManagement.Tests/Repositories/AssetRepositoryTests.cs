using AssetManagementApi.Models;
using AssetManagementApi.Repositories;
using Xunit;

namespace AssetManagement.Tests.Repositories;

public class AssetRepositoryTests
{
    [Fact]
    public async Task GetDetailAsync_ReturnsData()
    {
        await using var context = TestDbContextFactory.CreateContext();

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
    public async Task StatusExistsAsync_ReturnsExpectedResult()
    {
        await using var context = TestDbContextFactory.CreateContext();
        context.Statuses.Add(new Status { Id = 1, Name = "Active" });
        await context.SaveChangesAsync();

        var repository = new AssetRepository(context);

        var exists = await repository.StatusExistsAsync(1);
        var missing = await repository.StatusExistsAsync(999);

        Assert.True(exists);
        Assert.False(missing);
    }
}
