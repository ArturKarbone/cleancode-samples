namespace OrderProcessor.After;

internal record ProcessOrderResult
{
    public ProcessOrderResult(Guid orderId, string message)
    {
        this.OrderId = orderId;
        this.Message = message;
    }
    public string Message { get; init; }
    public Guid OrderId { get; init; }

    public bool IsValid =>
        string.IsNullOrEmpty(Message);

    //default
    public static ProcessOrderResult NotProcessable() =>
        new ProcessOrderResult(default, $"The order is not processable");

    public static ProcessOrderResult NotReadyForProcessing(Guid orderId) =>
       new ProcessOrderResult(orderId, $"The Order {orderId} isn't ready to process");

    public static ProcessOrderResult HasTooManyLineItems(Guid orderId) =>
       new ProcessOrderResult(orderId, $"The Order {orderId} has to many items");

    public static ProcessOrderResult Successful(Guid orderId) =>
        new ProcessOrderResult(orderId, default);
}

