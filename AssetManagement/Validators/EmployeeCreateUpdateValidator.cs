using AssetManagementApi.Dtos.Employees;
using FluentValidation;

namespace AssetManagementApi.Validators;

public class EmployeeCreateUpdateValidator : AbstractValidator<EmployeeCreateUpdateDto>
{
    public EmployeeCreateUpdateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}