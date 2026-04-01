using AssetManagementApi.Dtos.Employees;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public interface IEmployeeService
{
    Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesAsync();
    Task<EmployeeDetailDto?> GetEmployeeAsync(int id);
    Task<OperationResult<EmployeeDto>> CreateEmployeeAsync(EmployeeCreateUpdateDto dto);
    Task<OperationResult> UpdateEmployeeAsync(int id, EmployeeCreateUpdateDto dto);
    Task<OperationResult> DeleteEmployeeAsync(int id);
}
