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
            this.PlacedAt >= dateRange.StartDate && this.PlacedAt <= dateRange.EndDate;
    }

    class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
            public DateTime EndDate { get; }

            public decimal GetTotalSalesWithinDateRang()
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
