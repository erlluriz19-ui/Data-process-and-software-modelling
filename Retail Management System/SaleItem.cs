using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class SaleItem
    {
        public int quantity;
        public double subtotal;

        public double calculateSubtotal(double price, int quantity)
        {
            subtotal = Math.Round(price * quantity, 2);
            Console.WriteLine($"[SALE ITEM] {price:F2} x {quantity} = {subtotal:F2}");
            return subtotal;
        }
    }
}
