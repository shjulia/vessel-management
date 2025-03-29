using DomainModel.Entities.Vessels;
using FluentValidation;
using MediatR;

namespace API.Commands.Vessels;

public record RegisterVesselCommand(string Name, string Imo, string Type, decimal Capacity) : IRequest<Guid>;
    
public class RegisterVesselCommandValidator : AbstractValidator<RegisterVesselCommand>
{
    public RegisterVesselCommandValidator()
    {
        RuleFor(x => x.Type)
            .IsEnumName(typeof(VesselType));

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(1).WithMessage("Value must be at least 1.")
            .LessThanOrEqualTo(1000).WithMessage("Value must not exceed 1000.");
    }
}