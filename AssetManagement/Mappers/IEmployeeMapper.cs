using AssetManagementApi.Dtos.Employees;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public interface IEmployeeMapper
{
    EmployeeDto ToDto(Employee employee);
    EmployeeDetailDto ToDetailDto(Employee employee);
    Employee ToEntity(EmployeeCreateUpdateDto dto);
    void UpdateEntity(Employee employee, EmployeeCreateUpdateDto dto);
}
