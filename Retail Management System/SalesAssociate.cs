using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class SalesAssociate : User
    {
        public int employeeID;

        public SalesAssociate(int userID, string name, string email, string password, int employeeID)
            : base(userID, name, email, password)
        {
            this.employeeID = employeeID;
        }

        // FR1-FR5: Process an in-store sale with items and payment
        public bool processSale(Sale sale, Payment payment, List<SaleItem> items)
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("  [POS] No items to process.");
                return false;
            }
            foreach (SaleItem item in items)
                sale.items.Add(item);

            double total = sale.calculateTotal();
            Console.WriteLine($"  [POS] Sale total: ${total:F2}");
            payment.amount = total;

            bool success = payment.processPayment();
            if (success)
                generateReceipt(sale.saleID);
            return success;
        }

        // FR5: Generate receipt after successful sale
        public void generateReceipt(int saleID)
        {
            Console.WriteLine();
            Console.WriteLine("  --------------------------------");
            Console.WriteLine("       The Warehouse Group        ");
            Console.WriteLine($"   Sale ID   : {saleID}");
            Console.WriteLine($"   Served by : {name}");
            Console.WriteLine($"   Date      : {DateTime.Now:dd-MM-yyyy HH:mm}");
            Console.WriteLine("  --------------------------------");
        }
    }
}
