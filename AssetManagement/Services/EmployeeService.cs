using FluentValidation;
using AssetManagementApi.Dtos.Employees;
using AssetManagementApi.Mappers;
using AssetManagementApi.Repositories;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeMapper _employeeMapper;
    private readonly IValidator<EmployeeCreateUpdateDto> _validator;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IEmployeeMapper employeeMapper,
        IValidator<EmployeeCreateUpdateDto> validator)
    {
        _employeeRepository = employeeRepository;
        _employeeMapper = employeeMapper;
        _validator = validator;
    }

    public async Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(_employeeMapper.ToDto).ToList();
    }

    public async Task<EmployeeDetailDto?> GetEmployeeAsync(int id)
    {
        var employee = await _employeeRepository.GetDetailAsync(id);
        return employee is null ? null : _employeeMapper.ToDetailDto(employee);
    }

    public async Task<OperationResult<EmployeeDto>> CreateEmployeeAsync(EmployeeCreateUpdateDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult<EmployeeDto>.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        var employee = _employeeMapper.ToEntity(dto);
        await _employeeRepository.CreateAsync(employee);
        await _employeeRepository.SaveChangesAsync();

        return OperationResult<EmployeeDto>.Success(_employeeMapper.ToDto(employee));
    }

    public async Task<OperationResult> UpdateEmployeeAsync(int id, EmployeeCreateUpdateDto dto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
        {
            return OperationResult.NotFound();
        }

        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        _employeeMapper.UpdateEntity(employee, dto);
        await _employeeRepository.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteEmployeeAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
        {
            return OperationResult.NotFound();
        }

        await _employeeRepository.DeleteAsync(id);
        await _employeeRepository.SaveChangesAsync();

        return OperationResult.Success();
    }
}
