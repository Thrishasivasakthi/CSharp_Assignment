using TechShop.entity;
using System;
using System.Collections.Generic;

public class ReportService
{
    private readonly OrdersService _ordersService;

    public ReportService(OrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    public void GenerateSalesReport(DateTime startDate, DateTime endDate)
    {
        var orders = _ordersService.GetOrdersByDateRange(startDate, endDate, dbConnector: _ordersService.GetDatabaseConnector());

        decimal totalSales = 0;
        foreach (var order in orders)
        {
            if (order is Orders typedOrder)
            {
                totalSales += typedOrder.TotalAmount;
            }
            else
            {
                Console.WriteLine("Order is not of the expected type.");
            }
        }

        Console.WriteLine($"Total Sales from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {totalSales:C}");
    }

    public void GenerateProductSalesReport(DateTime startDate, DateTime endDate)
    {
        var orders = _ordersService.GetOrdersByDateRange(startDate, endDate, dbConnector: _ordersService.GetDatabaseConnector());
        Dictionary<int, decimal> productSales = new Dictionary<int, decimal>();

        foreach (var order in orders)
        {
            if (order is Orders typedOrder)
            {
                // Correctly invoke the GetOrderDetails method and store the result
                var orderDetails = typedOrder.GetOrderDetails() as List<OrderDetails>;

                // Ensure orderDetails is not null and contains data
                if (orderDetails != null && orderDetails.Count > 0)
                {
                    foreach (var orderDetail in orderDetails)
                    {
                        if (orderDetail is OrderDetails typedOrderDetail)
                        {
                            if (typedOrderDetail.Product != null)
                            {
                                int productID = typedOrderDetail.Product.ProductID;

                                if (!productSales.ContainsKey(productID))
                                    productSales[productID] = 0;

                                productSales[productID] += typedOrderDetail.Quantity * typedOrderDetail.Product.Price;
                            }
                            else
                            {
                                Console.WriteLine("Product is null in order details.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("OrderDetail is not of the expected type.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("OrderDetails is null or empty.");
                }
            }
            else
            {
                Console.WriteLine("Order is not of the expected type.");
            }
        }

        foreach (var productSale in productSales)
        {
            Console.WriteLine($"Product ID: {productSale.Key}, Total Sales: {productSale.Value:C}");
        }
    }
}
