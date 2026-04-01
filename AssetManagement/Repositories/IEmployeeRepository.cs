using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<Employee?> GetDetailAsync(int id);
}