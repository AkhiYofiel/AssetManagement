using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Dtos.Employees;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public class EmployeeMapper : IEmployeeMapper
{
    public EmployeeDto ToDto(Employee employee)
    {
        return new EmployeeDto(employee.Id, employee.FirstName, employee.LastName, employee.Email);
    }

    public EmployeeDetailDto ToDetailDto(Employee employee)
    {
        var assets = employee.Assets
            .Select(a => new AssetSummaryDto(a.Id, a.Name, a.SerialNumber, a.StatusId, a.Status?.Name ?? string.Empty))
            .ToList();

        return new EmployeeDetailDto(employee.Id, employee.FirstName, employee.LastName, employee.Email, assets);
    }

    public Employee ToEntity(EmployeeCreateUpdateDto dto)
    {
        return new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }

    public void UpdateEntity(Employee employee, EmployeeCreateUpdateDto dto)
    {
        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
    }
}
