using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class OrderItem
    {

        public int quantity;

        public double subtotal;

        public double calculateSubtotal(double price, int quantity)
        {

            if (quantity <= 0)
            {
                Console.WriteLine("[ORDER ITEM] Quantity must be at least 1.");
                return 0;
            }

            if (price < 0)
            {
                Console.WriteLine("[ORDER ITEM] Price cannot be negative.");
                return 0;
            }

            subtotal = Math.Round(price * quantity, 2);

            Console.WriteLine($"[ORDER ITEM] Subtotal calculated: " +
                              $"${price:F2} x {quantity} = ${subtotal:F2}");

            return subtotal;
        }

        public override string ToString()
        {
            return $"OrderItem[Qty={quantity}, Subtotal=${subtotal:F2}]";
        }
    }
}