using AssetManagementApi.Dtos.Status;
using FluentValidation;

namespace AssetManagementApi.Validators;

public class StatusCreateUpdateValidator : AbstractValidator<StatusCreateUpdateDto>
{
    public StatusCreateUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}