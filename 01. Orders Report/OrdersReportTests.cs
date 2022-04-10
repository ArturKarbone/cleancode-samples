using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrdersReport
{
    public class OrdersReportTests
    {
        [Fact]
        public void should_calculate_total_sales_within_range()
        {
            var orders = new List<Order>
            {
                new Order() { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
                new Order() { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
                new Order() { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
            };
            var report = new OrdersReport(
                orders,
                new DateRange()
                {
                    StartDate = DateTime.Parse("10/1/2022"),
                    EndDate = DateTime.Parse("10/2/2022")
                });

            var salesAmount = report.TotalSalesWithinDateRange();
            salesAmount.ShouldBe(300m);
        }
    }
}