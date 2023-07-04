namespace OrderProcessor.After;

internal class OrderProcessor
{

    //Null-forgiving operator !.
    //Null-conditional/Null-derefernce/Safe naviation opreator


    //Step 1 Guard clauses
    public void Process1(Order? order)
    {
        //early return principle
        if (order == null)
            return;

        if (!order.IsVerified)
            return;

        if (order.Items.Count() == 0)
            return;

        if (order.Items.Count() > 15)
        {
            throw new Exception("The Order " + order.Id + " has to many items");
        }

        if (order.StatusOld != "ReadyToProcess")
        {
            throw new Exception("The Order " + order.Id + " isn't ready to process");
        }

        order.IsProcessed = true;
    }

    //Step 2 Merge guards
    public void Process2(Order? order)
    {
        //early return principle
        if (order == null ||
            !order.IsVerified ||
            order.Items.Count() == 0)
            return;

        if (order.Items.Count() > 15)
        {
            throw new Exception("The Order " + order.Id + " has to many items");
        }

        if (order.StatusOld != "ReadyToProcess")
        {
            throw new Exception("The Order " + order.Id + " isn't ready to process");
        }

        order.IsProcessed = true;
    }

    //Step 3 Make clean name for guard
    public void Process3(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return;

        if (order!.Items.Count() > 15)
        {
            throw new Exception("The Order " + order.Id + " has to many items");
        }

        if (order.StatusOld != "ReadyToProcess")
        {
            throw new Exception("The Order " + order.Id + " isn't ready to process");
        }

        order.IsProcessed = true;

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }

    //Step 4 Specific Exceptions
    public void Process4(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return;

        if (order!.Items.Count() > 15)
        {
            throw new Exception("The Order " + order.Id + " has to many items");
        }

        if (order.StatusOld != "ReadyToProcess")
        {
            throw new Exception("The Order " + order.Id + " isn't ready to process");
        }

        order.IsProcessed = true;

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }

    //Step 5 Specific Exceptions
    public void Process5(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return;

        if (order!.Items.Count() > 15)
        {
            throw new OrderHasTooManyLineItemsException(order.Id);
        }

        if (order.StatusOld != "ReadyToProcess")
        {
            throw new OrderIsNotProcessableException(order.Id);
        }

        order.IsProcessed = true;

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }

    public const int ProcessableNumberOfLineItems = 15;
    //Step 6 Address Magic strings/numbers
    public void Process6(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return;

        if (order!.Items.Count() > ProcessableNumberOfLineItems)
        {
            throw new OrderHasTooManyLineItemsException(order.Id);
        }

        if (order.Status != OrderStatus.ReadyToProcess)
        {
            throw new OrderIsNotProcessableException(order.Id);
        }

        order.IsProcessed = true;

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }

    //Step 7 Replace return type with a result object
    public ProcessOrderResult Process7(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return ProcessOrderResult.NotProcessable();

        if (order!.Items.Count() > ProcessableNumberOfLineItems)
            return ProcessOrderResult.HasTooManyLineItems(order.Id);


        if (order.Status != OrderStatus.ReadyToProcess)
            return ProcessOrderResult.NotReadyForProcessing(order.Id);

        order.IsProcessed = true;

        return ProcessOrderResult.Successful(order.Id);

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }
}


