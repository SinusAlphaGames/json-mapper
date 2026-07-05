namespace Domain.Dummy;

public class DummyObject(string name)
{
    
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
}