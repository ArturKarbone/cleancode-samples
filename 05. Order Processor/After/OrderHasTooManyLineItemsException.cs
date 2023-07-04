namespace OrderProcessor.After
{
    internal sealed class OrderHasTooManyLineItemsException : Exception
    {
        //contextual information
        public Guid OrderId { get; init; }
        public OrderHasTooManyLineItemsException(Guid orderId) : 
            base($"The Order {orderId} has to many items") //formatting
        {
            this.OrderId = orderId;
        }
    }
}
