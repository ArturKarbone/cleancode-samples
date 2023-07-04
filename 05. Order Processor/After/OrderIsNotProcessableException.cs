namespace OrderProcessor.After;

internal sealed class OrderIsNotProcessableException : Exception
{

    //contextual information
    public Guid OrderId { get; init; }
    public OrderIsNotProcessableException(Guid orderId) :
        base($"The Order {orderId} isn't ready to process") //formattings
    {
        this.OrderId = orderId;
    }

}

