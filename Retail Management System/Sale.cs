using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class Sale
    {
        public int saleID;
        public DateTime date;
        public double totalAmount;
        public List<SaleItem> items = new List<SaleItem>();

        // FR3: Sum all SaleItem subtotals
        public double calculateTotal()
        {
            double total = 0;
            foreach (SaleItem item in items)
                total += item.subtotal;
            totalAmount = Math.Round(total, 2);
            return totalAmount;
        }

        // FR5: Generate receipt string
        public void generateReceipt()
        {
            Console.WriteLine($"  [SALE] Receipt for Sale ID {saleID} | Total: ${totalAmount:F2}");
        }
    }
}
