"# cleancode-samples" 

### Step #1 (Extract temp to query)

before:

```csharp
public decimal TotalSalesWithinDateRange()
{
    var orders_within_range = orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
    return orders_within_range.Sum(x => x.Amount);
}
```

**Code smell** - an indicator that there might be a problem.

Turn **orders_within_range** into a local or a private function, since the function contains details. Apply **extract temp to query** refactoring.

- One method with two lines turned into two methods one line each (Usually it is an improvement). 
- Try apply methods one line each (for a sake of an experiment)
- We can reuse it.
- Programmers read code. Extracting to a function gives us a hint - the details are not important + simplifies reading.

after:

```csharp
public decimal TotalSalesWithinDateRange()
{
    return orders_within_range().Sum(x => x.Amount);

    IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
}
```

or:

```csharp
public TotalSalesWithinDateRangeResponse Handle2(TotalSalesWithinDateRangeRequest request)
    => new() { Amount = orders_within_range(request).Sum(x => x.Amount) };

IEnumerable<Order> orders_within_range(TotalSalesWithinDateRangeRequest request) => 
    request.Orders.Where(x => x.PlacedBetween(request.DateRange));
```

### Step #2 (Tell. Don't ask)

Don't ask order about it's internal status. Send messages instead of that.

before:

```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
```

after:

```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

### Step #3 (Explicit naming of start/end dates)

Start and end dates make sense only together Let's call them DateRange explicitly (improves readability/shows an intent). Coupling is reduced now as well (easier to change). Abstraction from real life/value object/data clump.

Less coupling is always better.

before:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

after:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(new DateRange(startDate, endDate)));
```

### Step #4 (Great Responsibility for DateRange)

It is not order's responsibility to figure out if it is placed between date range, but is is a great responsibility for DateRange.

before:


```csharp
class Order
{
    public DateTime PlacedAt { get; set; }
    public decimal Amount { get; set; }
    //todo: cover with tests
    public bool PlacedBetween(DateRange dateRange) =>
        this.PlacedAt >= dateRange.StartDate && this.PlacedAt <= dateRange.EndDate;
}

class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
```

Now we can reuse **DateRange.Includes**. Exactly **Includes** not **PlacedBetween** since it is order's domain launguage. 

after:


```csharp
 class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool PlacedBetween(DateRange dateRange) =>
        dateRange.Indludes(this.PlacedAt);
}

 class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    //todo: cover with tests
    public bool Indludes(DateTime placedAt) =>
        placedAt >= StartDate && placedAt <= EndDate;
}
```

### Stp #5 expose Total Sales calculation as a separate use case with its own request and response

```csharp
public TotalSalesWithinDateRangeResponse Handle(TotalSalesWithinDateRangeRequest request)
{
    return new TotalSalesWithinDateRangeResponse { Amount = orders_within_range().Sum(x => x.Amount) };

    IEnumerable<Order> orders_within_range() => request.Orders.Where(x => x.PlacedBetween(request.DateRange));
}
```


### Step #6 Leverage target-typed new expression

before:

```csharp
public TotalSalesWithinDateRangeResponse Handle(TotalSalesWithinDateRangeRequest request)
{
    return new TotalSalesWithinDateRangeResponse { Amount = orders_within_range().Sum(x => x.Amount) };

    IEnumerable<Order> orders_within_range() => request.Orders.Where(x => x.PlacedBetween(request.DateRange));
}
```

afer:


```csharp
public TotalSalesWithinDateRangeResponse Handle(TotalSalesWithinDateRangeRequest request)
{
    return new() { Amount = orders_within_range().Sum(x => x.Amount) };

    IEnumerable<Order> orders_within_range() => request.Orders.Where(x => x.PlacedBetween(request.DateRange));
}
```



### Step #7 Leverage target-typed new expression in tests

before:

```csharp
var orders = new List<Order>
{
    new Order() { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
    new Order() { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
    new Order() { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
};
```

afer:


```csharp
var orders = new List<Order>
{
    new () { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
    new () { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
    new () { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
};
```

or even this:

```csharp
List<Order> orders = new()
{
    new () { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
    new () { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
    new () { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
};
```

### Step #8 Leverage target-typed new expression in use-case requests

before:

```csharp
var result = new OrdersReport().Handle(new OrdersReport.TotalSalesWithinDateRangeRequest(
        orders,
        new DateRange()
        {
            StartDate = DateTime.Parse("10/1/2022"),
            EndDate = DateTime.Parse("10/2/2022")
        }));

```

Note: highlight **new** and press F12 (go to definition). The same approach works for **var**.

afer:


```csharp

var result = new OrdersReport().Handle(
    new(
        orders,
        new ()
        {
            StartDate = DateTime.Parse("10/1/2022"),
            EndDate = DateTime.Parse("10/2/2022")
        }));

```
