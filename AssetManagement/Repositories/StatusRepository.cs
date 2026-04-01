using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    private AssetManagementContext Context => (AssetManagementContext)_context;

    public StatusRepository(AssetManagementContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Status>> GetAllAsync()
    {
        return await Context.Statuses.AsNoTracking().ToListAsync();
    }
}
