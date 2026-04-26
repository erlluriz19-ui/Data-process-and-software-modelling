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

        public double calculateTotal()
        {
            double total = 0;
            foreach (SaleItem item in items)
                total += item.subtotal;
            totalAmount = Math.Round(total, 2);
            return totalAmount;
        }

        public void generateReceipt ()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"   Sale ID : {saleID} | Total : {totalAmount}");
            Console.WriteLine("    Thank you for shopping with us!!");
            Console.WriteLine("-----------------------------------");
        }
    }
}
