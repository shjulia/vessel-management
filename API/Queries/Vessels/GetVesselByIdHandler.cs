using MediatR;
using ReadModel;
using ReadModel.Vessels;

namespace API.Queries.Vessels;

public class GetVesselByIdHandler(IReadVesselRepository vesselRepository) : IRequestHandler<GetVesselByIdQuery, GetVesselByIdResponse>
{
    public async Task<GetVesselByIdResponse> Handle(GetVesselByIdQuery command, CancellationToken cancellationToken)
    {
        return await vesselRepository.GetById(command.Id, cancellationToken);
    }
}
