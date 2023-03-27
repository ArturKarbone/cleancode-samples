using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersReport
{
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
            public TotalSalesWithinDateRangeRequest(IEnumerable<Order> orders, DateRange dateRange)
            {
                this.Orders = orders;
                this.DateRange = dateRange;
            }
            public IEnumerable<Order> Orders { get; set; }
            public DateRange DateRange { get; set; }
        }

        public class TotalSalesWithinDateRangeResponse
        {
            public decimal Amount { get; set; }
        }
    }

    class Order
    {
        public DateTime PlacedAt { get; set; }
        public decimal Amount { get; set; }

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

    namespace Initial
    {
        internal class OrdersReport
        {
            public OrdersReport(IEnumerable<Order> orders, DateTime startDate, DateTime endDate)
            {
                Orders = orders;
                StartDate = startDate;
                EndDate = endDate;
            }

            public IEnumerable<Order> Orders { get; }
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
            public DateTime PlacedAt { get; set; }
            public decimal Amount { get; set; }
            public bool PlacedBetween(DateRange dateRange) =>
                this.PlacedAt >= dateRange.StartDate && this.PlacedAt <= dateRange.EndDate;
        }

        class DateRange
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
