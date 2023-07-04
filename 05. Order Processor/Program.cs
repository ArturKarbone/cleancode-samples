// See https://aka.ms/new-console-template for more information
using OrderProcessor.After;

Console.WriteLine("Hello, World!");


var order = new Order() { IsVerified = true, Items = new List<LineItem> { new LineItem() }, Status = OrderStatus.ReadyToProcess };

var processor = new OrderProcessor.After.OrderProcessor();

var result = processor.Process7(order);

Console.WriteLine(result.ToString());