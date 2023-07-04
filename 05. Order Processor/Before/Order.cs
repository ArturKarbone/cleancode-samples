namespace OrderProcessor.Before;

internal class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsVerified { get; }
    public bool IsProcessed { get; set; }
    public IEnumerable<LineItem> Items { get; set; } = Enumerable.Empty<LineItem>();
    public string Status { get; internal set; }
}

