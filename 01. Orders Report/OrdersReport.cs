using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersReport
{
    internal class OrdersReport
    {
        private readonly IEnumerable<Order> orders;
        private readonly DateTime startDate;
        private readonly DateTime endDate;
        public OrdersReport(IEnumerable<Order> orders, DateTime startDate, DateTime endDate)
        {
            this.orders = orders;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public decimal TotalSalesWithinDateRange()
        {
            var orders_within_range = orders.Where(x => x.PlacedAt >= startDate && x.PlacedAt <= endDate);
            return orders_within_range.Sum(x => x.Amount);
        }
    }

    class Order
    {
        public DateTime PlacedAt { get; set; }
        public decimal Amount { get; set; }
    }
}
