using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

//file-scoped namespace
namespace OrdersReport.After;

internal class OrdersReport
{
    public TotalSalesWithinDateRangeResponse Handle(TotalSalesWithinDateRangeRequest request)
    {
        return new() { Amount = orders_within_range().Sum(x => x.Amount) };

        IEnumerable<Order> orders_within_range() => request.Orders.Where(x => x.PlacedBetween(request.DateRange));
    }

    public TotalSalesWithinDateRangeResponse Handle2(TotalSalesWithinDateRangeRequest request)
        => new() { Amount = orders_within_range(request).Sum(x => x.Amount) };

    IEnumerable<Order> orders_within_range(TotalSalesWithinDateRangeRequest request) =>
        request.Orders.Where(x => x.PlacedBetween(request.DateRange));

    public class TotalSalesWithinDateRangeRequest
    {
        // https://anthonygiretti.com/2022/12/02/introducing-c11-required-properties/

        [SetsRequiredMembers]
        public TotalSalesWithinDateRangeRequest(IEnumerable<Order> orders, DateRange dateRange)
        {
            this.Orders = orders;
            this.DateRange = dateRange;
        }
        required public IEnumerable<Order> Orders { get; init; }
        required public DateRange DateRange { get; init; }
    }

    public class TotalSalesWithinDateRangeResponse
    {
        public decimal Amount { get; set; }
    }
}

class Order
{
    public required DateTime PlacedAt { get; init; }
    public required decimal Amount { get; init; }

    public bool PlacedBetween(DateRange dateRange) =>
        dateRange.Includes(this.PlacedAt);
}

class DateRange
{
    private DateRange()
    {

    }

    public static DateRange Between(string startDate, string endDate) =>
        Between(DateTime.Parse(startDate), DateTime.Parse(endDate));

    public static DateRange Between(DateTime startDate, DateTime endDate) =>
        new() { StartDate = startDate, EndDate = endDate };

    required public DateTime StartDate { get; init; }
    required public DateTime EndDate { get; init; }

    //todo: cover with tests
    public bool Includes(DateTime placedAt) =>
      placedAt >= StartDate && placedAt <= EndDate;
}