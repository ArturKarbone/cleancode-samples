"# cleancode-samples" 

### Step #1

```
  public decimal TotalSalesWithinDateRange()
  {
      var orders_within_range = orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
      return orders_within_range.Sum(x => x.Amount);
  }
```

Turn **orders_within_range** into a local or a private function, since the function contains details. Extract from temp to query.
One method with two lines turned into two methods one line each (Usually it is an improvement). We can reuse it.
Programmers read code. Extracting to a function gives as a hint - the details are not important + simplifies reading.

```
  public decimal TotalSalesWithinDateRange()
  {
      return orders_within_range().Sum(x => x.Amount);

      IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
  }
```