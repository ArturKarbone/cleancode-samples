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

Turn **orders_within_range** into a local or a private function, since the function contains details. Extract temp to query.
One method with two lines turned into two methods one line each (Usually it is an improvement). We can reuse it.
Programmers read code. Extracting to a function gives us a hint - the details are not important + simplifies reading.

after:

```csharp
  public decimal TotalSalesWithinDateRange()
  {
      return orders_within_range().Sum(x => x.Amount);

      IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
  }
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

Start and end dates make sense only together. Let's call them DateRange explicitly (improves readability/shows an intent). Coupling is reduced now as well (easier to change).

before:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

after:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

### Stp #4 expose Total Sales calculation as a separate use case with its own request and response

```csharp
public TotalSalesWithinDateRangeResponse Handle(TotalSalesWithinDateRangeRequest request)
{
    return new TotalSalesWithinDateRangeResponse { Amount = orders_within_range().Sum(x => x.Amount) };

    IEnumerable<Order> orders_within_range() => request.Orders.Where(x => x.PlacedBetween(request.DateRange));
}
```