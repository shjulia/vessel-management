namespace DomainModel.Entities.Vessels;

public interface IVesselRepository : IRepository<Vessel>
{
    public Task<Vessel?> FindByImo(IMO id, CancellationToken cancellationToken = default);
}