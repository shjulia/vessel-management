using DomainModel.Entities.Vessels;
using MediatR;

namespace API.Commands.Vessels;

public class RegisterVesselHandler(IVesselRepository vesselRepository) : IRequestHandler<RegisterVesselCommand, Guid>
{
    public async Task<Guid> Handle(RegisterVesselCommand command, CancellationToken cancellationToken)
    {
        var existingVesselWithImo = await vesselRepository.FindByImo(new IMO(command.Imo), cancellationToken);
        
        if (existingVesselWithImo != null)
        {
            throw new InvalidOperationException($"A vessel with IMO {command.Imo} already exists.");
        }
        
        var vessel = new Vessel(Guid.NewGuid(), command.Name, new IMO(command.Imo), Enum.Parse<VesselType>(command.Type), command.Capacity);
        
        vesselRepository.Add(vessel);
        await vesselRepository.Save(cancellationToken);
        
        return vessel.Id;
    }
}