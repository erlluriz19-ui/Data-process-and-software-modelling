using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class OrderItem
    {
        public int quantity;
        public double subtotal;

        public double calculateSubtotal(double price, int quantity)
        {
            subtotal = Math.Round(price * quantity, 2);
            return subtotal;
        }
    }

    public class Order
    {
        public int orderID;
        public DateTime orderDate;
        public OrderStatusEnum status;
        public List<OrderItem> items = new List<OrderItem>();

        public Order(int orderID)
        {
            this.orderID = orderID;
            this.orderDate = DateTime.Now;
            this.status = OrderStatusEnum.Pending;
        }

        // FR8: Place order - transition from Pending to Confirmed
        public bool placeOrder(List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                status = OrderStatusEnum.Failed;
                Console.WriteLine("  [ORDER] Cannot place empty order.");
                return false;
            }
            status = OrderStatusEnum.Confirmed;
            Console.WriteLine($"  [ORDER] Order {orderID} confirmed. Items: {products.Count}");
            return true;
        }

        // Cancel order if Pending or Confirmed
        public void cancelOrder(int orderID)
        {
            if (status == OrderStatusEnum.Pending || status == OrderStatusEnum.Confirmed)
            {
                status = OrderStatusEnum.Cancelled;
                Console.WriteLine($"  [ORDER] Order {orderID} cancelled.");
            }
            else
            {
                Console.WriteLine($"  [ORDER] Order {orderID} cannot be cancelled (status: {status}).");
            }
        }
    }
}
