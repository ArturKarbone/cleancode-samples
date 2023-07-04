namespace OrderProcessor.Before;

internal class OrderProcessor
{
    //too-nested replace with early return
    //string interpolation over concatenation
    //specific exceptions with context
    //enums vs magic strings/numbers
    public void Process(Order order)
    {
        if (order != null)
        {
            if (order.IsVerified)
            {
                if (order.Items.Count() > 0)
                {
                    if (order.Items.Count() > 15)
                    {
                        throw new Exception("The Order " + order.Id + " has to many items");
                    }

                    if (order.Status != "ReadyToProcess")
                    {
                        throw new Exception("The Order " + order.Id + " isn't ready to process");
                    }

                    order.IsProcessed = true;
                }
            }
        }
    }
}


