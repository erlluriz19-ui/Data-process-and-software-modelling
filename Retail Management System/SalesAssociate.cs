using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System 
{
    //inherits login(), logout() and user fields from User class
    public class SalesAssociate : User
    {
        public int employeeID;

        //Constructor that calls base() to initialized inherited fields from User class
        public SalesAssociate(int userID, string name, string email, string password, int employeeID)
            : base (userID, name, email, password)
        {
            this.employeeID = employeeID;
        }

        public bool processSale(Sale sale, Payment payment, List<SaleItem> items)
        {   
            //rejects an empty sale immediately
            if (items == null || items.Count == 0 )
            {
                Console.WriteLine("[Point of Sale]: No items to process");
                return false;
            }
            //loops through each item 
            foreach (SaleItem item in items)
            {
                //Add each scanned item to the Sale
                sale.items.Add(item);
            }
            //sums all SaleItem subtotals
            double total = sale.calculateTotal(); 
            payment.amount = total;
            //calls processPayment() to validate and process the payment.
            bool success = payment.processPayment();

            if (success) generateReceipt(sale.saleID);
            return success;
        }

        public void generateReceipt(int saleID)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("      The Warehouse Group       ");
            Console.WriteLine($"   Sale ID : {saleID} | Served by : {name}");
            Console.WriteLine($"   Date : {DateTime.Now: dd-MM-yyyy HH:mm}");
            Console.WriteLine("--------------------------------");
        }
    }
}
