using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents a single line item inside an online Order.
    // Part of the Composition relationship — Order ◆──── OrderItem (1..*).
    // Created and owned by an Order object.
    // FR8
    public class OrderItem
    {
        public int quantity;
        public double subtotal;

        // FR8 — Calculates the line subtotal (price x quantity) and stores it.
        // Rounds to 2 decimal places to avoid floating-point precision issues.
        public double calculateSubtotal(double price, int quantity)
        {
            // Guard: quantity and price must be valid
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