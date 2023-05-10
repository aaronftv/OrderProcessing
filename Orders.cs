public class Orders
{
    private List<Order> orderList = new List<Order>();

    public List<Order> OrderList { get => orderList; }

    public Orders(string fileToParse) => orderList = ParseFile(fileToParse);

    private List<Order> ParseFile(string fileToParse)
    {
        List<Order> orderList = new List<Order>();
        var fileLines = File.ReadLines(fileToParse);
        var lineTypeIdentifier = string.Empty;

        foreach(var fileline in fileLines)
        {
            lineTypeIdentifier = fileline.Substring(0,3);

            if(lineTypeIdentifier == "100")
            {
                orderList.Add(new Order());
            }

            switch(lineTypeIdentifier)
            {
                case "100":
                    if(int.TryParse(fileline.Substring(3,10).Trim(), out int orderNumber))
                    {
                        orderList[^1].OrderNumber = orderNumber;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += "\r\n    OrderNumber has an invalid format.";
                    }

                    if(int.TryParse(fileline.Substring(13,5).Trim(), out int totalItems))
                    {
                        orderList[^1].TotalItems = totalItems;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += "\r\n    TotalItems has an invalid format.";
                    }

                    if(float.TryParse(fileline.Substring(18,10).Trim(), out float totalCost))
                    {
                        orderList[^1].TotalCost = totalCost;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += "\r\n    TotalCost has an invalid format.";
                    }

                    if(DateTime.TryParse(fileline.Substring(28,19).Trim(), out DateTime orderDate))
                    {
                        orderList[^1].OrderDate = orderDate;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += "\r\n    OrderDate has an invalid format.";
                    }

                    orderList[^1].CustomerName = fileline.Substring(47,50).Trim();
                    orderList[^1].CustomerPhone = fileline.Substring(97,30).Trim();
                    orderList[^1].CustomerEmail = fileline.Substring(127,50).Trim();

                    orderList[^1].Paid = fileline.Substring(177,1).Trim() == "1" ? true : false;
                    orderList[^1].Shipped = fileline.Substring(178,1).Trim() == "1" ? true : false;
                    orderList[^1].Completed = fileline.Substring(179,1).Trim() == "1" ? true : false;
                 break;
                case "200":
                    orderList[^1].Address.AddressLine1 = fileline.Substring(3,50).Trim();
                    orderList[^1].Address.AddressLine2 = fileline.Substring(53,50).Trim();
                    orderList[^1].Address.City = fileline.Substring(103,50).Trim();
                    orderList[^1].Address.State = fileline.Substring(153,2).Trim();
                    orderList[^1].Address.Zip = fileline.Substring(155,10).Trim();
                 break;
                case "300":
                    orderList[^1].OrderDetails.Add(new OrderDetail());

                    if(int.TryParse(fileline.Substring(3,2).Trim(), out int lineNumber))
                    {
                        orderList[^1].OrderDetails[^1].LineNumber = lineNumber;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += "\r\n    LineNumber has an invalid format.";
                    }

                    if(int.TryParse(fileline.Substring(5,5).Trim(), out int quantity))
                    {
                        orderList[^1].OrderDetails[^1].Quantity = quantity;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += $"\r\n    OrderDetail {lineNumber} Quantity has an invalid format.";
                    }

                    if(float.TryParse(fileline.Substring(10,10).Trim(), out float costEach))
                    {
                        orderList[^1].OrderDetails[^1].CostEach = costEach;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += $"\r\n    OrderDetail {lineNumber} CostEach has an invalid format.";
                    }

                    if(float.TryParse(fileline.Substring(20,10).Trim(), out float orderDetailTotalCost))
                    {
                        orderList[^1].OrderDetails[^1].TotalCost = orderDetailTotalCost;
                    }
                    else
                    {
                        orderList[^1].ParseSuccessful = false;
                        orderList[^1].ParseErrorMessage += $"\r\n    OrderDetail {lineNumber} TotalCost has an invalid format.";
                    }

                    orderList[^1].OrderDetails[^1].Description = fileline.Substring(30,50).Trim();
                 break;
            }
        }

        return orderList;
    }
}

public class Order
{
    public int? OrderNumber { get; set; }
    public int? TotalItems { get; set; }
    public float? TotalCost { get; set; }
    public DateTime? OrderDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public bool Paid { get; set; }
    public bool Shipped { get; set; }
    public bool Completed { get; set; }
    public Address Address { get; set; } = new Address();
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public bool ParseSuccessful { get; set; } = true;
    public string ParseErrorMessage { get; set; }
}

public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}

public class OrderDetail
{
    public int? LineNumber { get; set; }
    public int? Quantity { get; set; }
    public float? CostEach { get; set; }
    public float? TotalCost { get; set; }
    public string Description { get; set; }
}