using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//file-scopedd namespace declaration

namespace OrdersReport.Before;

internal class OrdersReport
{
    public OrdersReport(IEnumerable<Order> orders, DateTime startDate, DateTime endDate)
    {
        Orders = orders;
        StartDate = startDate;
        EndDate = endDate;
    }

    public IEnumerable<Order> Orders { get; init; }
    public DateTime StartDate { get; private set; }
    //Readonly property
    //https://developerpublish.com/getter-only-read-only-auto-properties-in-c-6-0/
    //https://www.pluralsight.com/guides/declare-and-initialize-read-only-auto-properties-in-c
    //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
    //https://www.c-sharpcorner.com/UploadFile/3d39b4/property-in-C-Sharp/

    //C# 6.0
    //https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/october/csharp-the-new-and-improved-csharp-6-0
    public DateTime EndDate { get; }

    public decimal GetTotalSalesWithinDateRange()
    {
        var orders_within_range = Orders.Where(x => x.PlacedAt >= StartDate && x.PlacedAt <= EndDate);
        return orders_within_range.Sum(x => x.Amount);
    }

    public decimal total_sales_within_date_range()
    {
        var orders_within_range = Orders.Where(x => x.PlacedAt >= StartDate && x.PlacedAt <= EndDate);
        return orders_within_range.Sum(x => x.Amount);
    }
}

class Order
{
    public required DateTime PlacedAt { get; init; }
    public required decimal Amount { get; init; }
    public bool PlacedBetween(DateRange dateRange) =>
        this.PlacedAt >= dateRange.StartDate && this.PlacedAt <= dateRange.EndDate;
}

class DateRange
{
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}


