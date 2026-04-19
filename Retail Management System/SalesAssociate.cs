using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents an in-store Sales Associate who operates the POS terminal.
    // Extends User (Inheritance) — reuses login, logout, and shared account fields.
    // FR1, FR2, FR3, FR4, FR5
    public class SalesAssociate : User
    {
        private int _employeeID;

        public int EmployeeID
        {
            get { return _employeeID; }
            private set { _employeeID = value; }
        }

        // Calls base(...) to initialise inherited User fields
        public SalesAssociate(int userId, string name, string email,
                              string password, int employeeID)
            : base(userId, name, email, password)
        {
            _employeeID = employeeID;
        }

        // FR1, FR3, FR4, FR5 — Processes an in-store sale.
        // Sale and Payment are passed in from Program.cs (low coupling).
        // Adds items to the sale, calculates the total, processes payment,
        // updates inventory, and generates a receipt on success.
        public bool processSale(Sale sale, Payment payment, List<SaleItem> items)
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("[POS] Cannot process a sale with no items. " +
                                  "Please scan at least one product.");
                return false;
            }

            Console.WriteLine($"[POS] Processing sale with {items.Count} item(s)...");

            // Add each scanned item to the sale record
            foreach (SaleItem item in items)
                sale.items.Add(item);

            // FR3 — Calculate total and assign to payment amount
            double total = sale.calculateTotal(items);
            payment.amount = total;

            Console.WriteLine($"[POS] Sale total calculated: ${total:F2}");

            bool paymentSuccess = payment.processPayment();

            if (paymentSuccess)
            {
                // FR5 — Stock update happens in Program.cs after this returns
                Console.WriteLine("[POS] Updating inventory stock levels in real time...");
                Console.WriteLine($"[POS] Sale #{sale.saleID} completed successfully.");
                generateReceipt(sale.saleID);
                return true;
            }
            else
            {
                Console.WriteLine("[POS] Sale could not be completed — payment failed.");
                return false;
            }
        }

        // FR4 — Prints a receipt for the completed sale
        public void generateReceipt(int saleID)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("          THE WAREHOUSE GROUP             ");
            Console.WriteLine($"  Receipt for Sale ID : {saleID}");
            Console.WriteLine($"  Served by           : {Name}");
            Console.WriteLine($"  Date                : {DateTime.Now:yyyy-MM-dd HH:mm}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("  Thank you for shopping with us!");
            Console.WriteLine("------------------------------------------");
        }

        public override string ToString()
        {
            return $"SalesAssociate[EmployeeID={_employeeID}, Name={Name}]";
        }
    }
}