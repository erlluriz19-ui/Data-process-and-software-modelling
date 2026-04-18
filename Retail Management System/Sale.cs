using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Sale
    {

        public int saleID;

        public DateTime date;

        public double totalAmount;

        public List<SaleItem> items = new List<SaleItem>();

        public Payment payment;

        public double calculateTotal(List<SaleItem> items)
        {

            if (items == null || items.Count == 0)
            {
                Console.WriteLine("[POS] No items found — total is $0.00.");
                totalAmount = 0;
                return 0;
            }

            double total = 0;

            foreach (SaleItem item in items)
            {
                total += item.subtotal;
            }

            totalAmount = Math.Round(total, 2);

            Console.WriteLine($"[POS] Grand total calculated: ${totalAmount:F2}");
            return totalAmount;
        }

        public void generateReceipt()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("          The Warehouse Group            ");
            Console.WriteLine("==========================================");
            Console.WriteLine($"  Sale ID   : {saleID}");
            Console.WriteLine($"  Date      : {date:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"  {"Item",-20} {"Qty",4} {"Subtotal",10}");
            Console.WriteLine("------------------------------------------");

            foreach (SaleItem item in items)
            {
                Console.WriteLine($"  {"Product",-20} {item.quantity,4} " +
                                  $"${item.subtotal,9:F2}");
            }

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"  {"TOTAL",-20} {"",4} ${totalAmount,9:F2}");
            Console.WriteLine("==========================================");
            Console.WriteLine("  Thank you for shopping at              ");
            Console.WriteLine("  The Warehouse Group!                   ");
            Console.WriteLine("==========================================");

            Console.WriteLine($"[RECEIPT] Receipt for SaleID {saleID} printed successfully.");
        }

        public override string ToString()
        {
            return $"Sale[ID={saleID}, Date={date:yyyy-MM-dd}, " +
                   $"Total=${totalAmount:F2}, Items={items.Count}]";
        }
    }
}