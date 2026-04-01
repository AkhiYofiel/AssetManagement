using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private AssetManagementContext Context => (AssetManagementContext)_context;

    public EmployeeRepository(AssetManagementContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await Context.Employees.AsNoTracking().ToListAsync();
    }

    public Task<Employee?> GetDetailAsync(int id)
    {
        return Context.Employees
            .AsNoTracking()
            .Include(e => e.Assets)
            .ThenInclude(a => a.Status)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
