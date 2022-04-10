using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersReport
{
    internal class OrdersReport
    {
        private readonly IEnumerable<Order> orders;
        private readonly DateRange dateRange;

        public OrdersReport(IEnumerable<Order> orders, DateRange dateRange)
        {
            this.orders = orders;
            this.dateRange = dateRange;
        }

        public decimal TotalSalesWithinDateRange()
        {
            return orders_within_range().Sum(x => x.Amount);

            IEnumerable<Order> orders_within_range() => orders.Where(x => x.PlacedBetween(dateRange));
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
