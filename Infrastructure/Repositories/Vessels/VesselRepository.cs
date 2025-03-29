using DomainModel.Entities.Vessels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Vessels;

public class VesselRepository(AppDbContext context) : Repository<Vessel>(context), IVesselRepository
{
    public async Task<Vessel?> FindByImo(IMO imo, CancellationToken cancellationToken)
    {
        return await Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Imo.Value == imo.Value, cancellationToken);
    }
}