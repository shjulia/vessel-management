using MediatR;
using ReadModel.Vessels;

namespace API.Queries.Vessels;

public record GetAllVesselsQuery : IRequest<List<GetAllVesselsResponse>>;