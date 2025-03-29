namespace DomainModel.Entities.Vessels;

public class IMO
{
    public string Value { get; private set; }

    public IMO(string value)
    {
        Value = value;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is IMO other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
}