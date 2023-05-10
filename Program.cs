var orders = new Orders("OrderFile.txt");

foreach(var order in orders.OrderList)
{
    Console.WriteLine("--------------------");
    Console.WriteLine($"Order Number: {order.OrderNumber} | Total Items: {order.TotalItems} | Total Cost: {order.TotalCost} | Order Date: {order.OrderDate}");
    Console.WriteLine($"Customer Name: {order.CustomerName} | Customer Phone: {order.CustomerPhone} | Customer Email: {order.CustomerEmail}");
    Console.WriteLine($"Paid: {order.Paid} | Shipped: {order.Shipped} | Completed: {order.Completed}");
    Console.WriteLine($"Address Line 1: {order.Address.AddressLine1} | Address Line 2: {order.Address.AddressLine2}");
    Console.WriteLine($"City: {order.Address.City} | State: {order.Address.State} | Zip: {order.Address.Zip}");

    if(order.OrderDetails.Count > 0) Console.WriteLine($"Order Details: ");
    foreach(var detail in order.OrderDetails)
    {
        Console.WriteLine($"    Line Number: {detail.LineNumber} | Quantity: {detail.Quantity} | Cost Each: {detail.CostEach} | Total Cost: {detail.TotalCost} | Description: {detail.Description}");
    }

    Console.WriteLine($"Parse Successful: {order.ParseSuccessful}");
    if(!order.ParseSuccessful) Console.WriteLine($"Parse Error Message: {order.ParseErrorMessage}");

    Console.WriteLine("--------------------");
}