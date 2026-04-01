using AssetManagementApi.Dtos.SoftwareLicenses;
using FluentValidation;

namespace AssetManagementApi.Validators;

public class SoftwareLicenseCreateUpdateValidator : AbstractValidator<SoftwareLicenseCreateUpdateDto>
{
    public SoftwareLicenseCreateUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LicenseKey).NotEmpty();
        RuleFor(x => x.ExpirationDate).NotEqual(default(DateTime));
    }
}