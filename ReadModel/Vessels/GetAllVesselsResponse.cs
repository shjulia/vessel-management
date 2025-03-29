namespace ReadModel.Vessels;

public record GetAllVesselsResponse(Guid Id, string Name, string Imo, string Type, decimal Capacity);