using MediatR;
using ReadModel.Vessels;

namespace API.Queries.Vessels;

public record GetVesselByIdQuery(Guid Id) : IRequest<GetVesselByIdResponse>;