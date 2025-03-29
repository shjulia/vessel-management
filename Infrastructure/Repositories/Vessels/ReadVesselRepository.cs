using DomainModel.Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using ReadModel;
using ReadModel.Vessels;

namespace Infrastructure.Repositories.Vessels;

public class ReadVesselRepository(AppDbContext context) : IReadVesselRepository
{
    public async Task<List<GetAllVesselsResponse>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Vessels
            .Select(v => new GetAllVesselsResponse(v.Id, v.Name, v.Imo.Value, v.Type.ToString(), v.Capacity))
            .ToListAsync(cancellationToken);
    }

    public async Task<GetVesselByIdResponse> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var vessel = await context.Vessels.FindAsync(id, cancellationToken);
        if (vessel == null)
        {
            throw new EntityNotFoundException($"No vessel found with ID {id}.");
        }

        return new GetVesselByIdResponse(vessel.Id, vessel.Name, vessel.Imo.Value, vessel.Type.ToString(), vessel.Capacity);
    }
}