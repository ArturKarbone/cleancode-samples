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
            //Target-typed new expression
            //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new

            List<Order> orders = new()
            {
                new () { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
                new () { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
                new () { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
            };

            var result = new OrdersReport().Handle(
                new(
                    orders,
                    DateRange.Between("10/1/2022", "10/2/2022")
                    ));

            result.Amount.ShouldBe(300m);
        }

        [Fact]
        public void should_calculate_total_sales_within_range_for_initial_code()
        {
            List<Initial.Order> orders = new()
            {
                new () { Amount = 100, PlacedAt = DateTime.Parse("10/1/2022") },
                new () { Amount = 200, PlacedAt = DateTime.Parse("10/2/2022") },
                new () { Amount = 400, PlacedAt = DateTime.Parse("10/3/2022") }
            };

            var orderReport = new Initial.OrdersReport(orders, DateTime.Parse("10/1/2022"), DateTime.Parse("10/2/2022"));

            orderReport.GetTotalSalesWithinDateRange().ShouldBe(300m);
        }
    }
}