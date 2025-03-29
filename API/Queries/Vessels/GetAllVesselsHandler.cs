using MediatR;
using ReadModel;
using ReadModel.Vessels;

namespace API.Queries.Vessels;

public class GetAllVesselsHandler(IReadVesselRepository vesselRepository)
    : IRequestHandler<GetAllVesselsQuery, List<GetAllVesselsResponse>>
{

    public async Task<List<GetAllVesselsResponse>> Handle(GetAllVesselsQuery request, CancellationToken cancellationToken)
    {
        return await vesselRepository.GetAll(cancellationToken);
    }
}