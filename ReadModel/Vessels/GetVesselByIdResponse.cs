namespace ReadModel.Vessels;

public record GetVesselByIdResponse(Guid Id, string Name, string Imo, string Type, decimal Capacity);