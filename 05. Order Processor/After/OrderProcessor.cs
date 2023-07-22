namespace OrderProcessor.After;

internal class OrderProcessor
{

    //Null-forgiving operator !.
    //Null-conditional/Null-derefernce/Safe naviation opreator


    //Null-propagating 
    //https://www.codingame.com/playgrounds/5107/null-propagating-operator-in-c#:~:text=Null%20propagating%20operator%20(%3F)%20is,if%20this%20is%20good%20code.
    //https://stackoverflow.com/questions/54724304/what-does-null-statement-mean


    //Step 1. Introduce Guard clauses
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

    //Step 2. Merge the guards
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

    //Step 3. Make a clean name for the guard
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

    //Step 4. Enforc specific exceptions
    public void Process4(Order? order)
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
    //Step 5. Address magic strings/numbers
    public void Process5(Order? order)
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

    //Step 6. Replace return type with a result object
    public ProcessOrderResult Process6(Order? order)
    {
        //early return principle
        if (!IsOrderProcessable())
            return ProcessOrderResult.NotProcessable();

        if (order!.Items.Count() > ProcessableNumberOfLineItems)
        {
            return ProcessOrderResult.HasTooManyLineItems(order.Id);
        }

        if (order.Status != OrderStatus.ReadyToProcess)
        {
            return ProcessOrderResult.NotReadyForProcessing(order.Id);
        }

        order.IsProcessed = true;

        return ProcessOrderResult.Successful(order.Id);

        bool IsOrderProcessable() =>
            order is not null &&
            order.IsVerified &&
            order.Items.Any();
    }

    //Step 7. Make ifs without braces
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

    //Step 8. Leverage tell, don't ask principle. Make IsProcessable order's responsibility.
    public ProcessOrderResult Process8(Order? order)
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

        bool IsOrderProcessable() => order?.IsProcessable() ?? false;
    }
}