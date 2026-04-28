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

        //loops through every SaleItem and sums their subtotals for grand total
        public double calculateTotal()
        {
            double total = 0;
            foreach (SaleItem item in items)
                total += item.subtotal;
            totalAmount = Math.Round(total, 2);
            return totalAmount;
        }
    }
}
