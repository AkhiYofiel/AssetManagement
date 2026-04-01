using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Repositories;
using FluentValidation;

namespace AssetManagementApi.Validators;

public class AssetUpdateValidator : AbstractValidator<AssetUpdateDto>
{
    private readonly IAssetRepository _assetRepository;

    public AssetUpdateValidator(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.WarrantyCard).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.WarrantyCard!.Provider).NotEmpty();
                RuleFor(x => x.WarrantyCard!.EndDate).GreaterThanOrEqualTo(x => x.WarrantyCard!.StartDate);
            });
        RuleFor(x => x.StatusId).MustAsync(StatusExistsAsync).WithMessage("StatusId is invalid.");
        RuleFor(x => x.EmployeeId).MustAsync(EmployeeExistsAsync).When(x => x.EmployeeId.HasValue).WithMessage("EmployeeId is invalid.");
    }

    private Task<bool> StatusExistsAsync(int statusId, CancellationToken cancellationToken)
    {
        return _assetRepository.StatusExistsAsync(statusId);
    }

    private Task<bool> EmployeeExistsAsync(int? employeeId, CancellationToken cancellationToken)
    {
        return employeeId.HasValue ? _assetRepository.EmployeeExistsAsync(employeeId.Value) : Task.FromResult(true);
    }
}