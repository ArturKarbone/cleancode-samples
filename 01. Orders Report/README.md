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

Turn **orders_within_range** into a local or a private function, since the function contains details. Extract from temp to query.
One method with two lines turned into two methods one line each (Usually it is an improvement). We can reuse it.
Programmers read code. Extracting to a function gives as a hint - the details are not important + simplifies reading.

after:

```csharp
  public decimal TotalSalesWithinDateRange()
  {
      return orders_within_range().Sum(x => x.Amount);

      IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
  }
```

### Step #2 (Tell. Don't ask)

Don't as order about it's internal status. Send messages instead of that.

before:

```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
```

after:

```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

### Step #3 (Explicit nanming of start/end dates)

Start and end dates make sense only together. Let's call them DateRange explicitly. Coupling is reduced now also.

before:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```

after:


```csharp
IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(startDate, endDate));
```