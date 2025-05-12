namespace Codelabs.UI.Components.InternalTypes;

public class CounterService
{
    public int Counter { get; set; } = 0;
    public void Reset() => Counter = 0;
    public void Inc() => Counter++;
}