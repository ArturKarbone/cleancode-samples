namespace OrderProcessor.After
{
    internal class Order
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool IsVerified { get; set; }
        public bool IsProcessed { get; set; }
        public IEnumerable<LineItem> Items { get; set; } = Enumerable.Empty<LineItem>();
        public OrderStatus Status { get; internal set; } = OrderStatus.Pending;
        public string StatusOld { get; internal set; }
    }
}
