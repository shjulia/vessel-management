using DomainModel.Entities.Vessels;
using MediatR;

namespace API.Commands.Vessels;

public class UpdateVesselHandler(IVesselRepository vesselRepository) : IRequestHandler<UpdateVesselCommand>
{
    public async Task Handle(UpdateVesselCommand command, CancellationToken cancellationToken)
    {
        var vessel = await vesselRepository.GetById(command.Id, cancellationToken);

        var imo = new IMO(command.Imo);
        if (vessel.Imo.Value != command.Imo && await vesselRepository.FindByImo(imo, cancellationToken) != null)
        {
            throw new InvalidOperationException($"A vessel with IMO {command.Imo} already exists.");
        }
        
        vessel.Update(command.Name, imo, Enum.Parse<VesselType>(command.Type), command.Capacity);
        await vesselRepository.Save(cancellationToken);
    }
}
