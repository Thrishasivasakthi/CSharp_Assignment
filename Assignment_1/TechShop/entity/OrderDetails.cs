using System;
using TechShop.exception;

namespace TechShop.entity
{
    public class OrderDetails
    {
        private int orderDetailId;
        private Orders order;
        private Products product;
        private int quantity;
        private decimal discount;

        public int OrderDetailID { get => orderDetailId; set => orderDetailId = value; }

        // Composition relationship - checks if Order is null (incomplete order)
        public Orders Order
        {
            get => order;
            set
            {
                if (value == null) throw new IncompleteOrderException("Order cannot be null.");
                order = value;
            }
        }

        // Composition relationship - checks if Product is null (incomplete order detail)
        public Products Product
        {
            get => product;
            set
            {
                if (value == null) throw new IncompleteOrderException("Product cannot be null.");
                product = value;
            }
        }

        // Validates quantity
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value <= 0) throw new exception.InvalidDataException("Quantity must be positive.");
                quantity = value;
            }
        }

        public object UnitPrice { get; internal set; }

        // Calculates the subtotal for the order detail (product price * quantity)
        public decimal CalculateSubtotal()
        {
            if (Product == null) throw new IncompleteOrderException("Cannot calculate subtotal. Product is missing.");
            return Product.Price * Quantity * (1 - discount);
        }

        // Displays the order detail information
        public void GetOrderDetailInfo()
        {
            if (Product == null || Order == null) throw new IncompleteOrderException("Incomplete order detail. Missing product or order.");
            Console.WriteLine($"OrderID: {Order.OrderID}, Product: {Product.ProductName}, Quantity: {Quantity}, Subtotal: ${CalculateSubtotal()}");
        }

        // Update the quantity of the product in this order detail
        public void UpdateQuantity(int newQuantity) => Quantity = newQuantity;

        // Apply discount to the order detail
        public void AddDiscount(decimal percent)
        {
            if (percent < 0 || percent > 1) throw new exception.InvalidDataException("Invalid discount percent.");
            discount = percent;
        }
    }
}
