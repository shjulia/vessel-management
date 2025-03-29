using ReadModel.Vessels;

namespace ReadModel;

public interface IReadVesselRepository
{
    Task<List<GetAllVesselsResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<GetVesselByIdResponse> GetById(Guid id, CancellationToken cancellationToken  = default);
}