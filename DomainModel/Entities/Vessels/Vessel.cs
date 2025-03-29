namespace DomainModel.Entities.Vessels;

public class Vessel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IMO Imo { get; private set; }
    public VesselType Type { get; private set; }
    public decimal Capacity { get; private set; }

    public Vessel() { }
    
    public Vessel(Guid id, string name, IMO imo, VesselType type, decimal capacity)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Imo = imo ?? throw new ArgumentNullException(nameof(imo));
        Type = type;
        Capacity = capacity;
    }

    public void Update(string name, IMO imo, VesselType type, decimal capacity)
    {
        Name = name;
        Imo = imo;
        Type = type;
        Capacity = capacity;
    }
}