using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System.Classes
{
    public class SalesAssociate : User
    {
        public int employeeID;

        public SalesAssociate(int userID, string name, string email, string password, int employeeID)
            : base (userID, name, email, password)
        {
            this.employeeID = employeeID;
        }

        public bool processSale(SalesAssociate sale, Payment payment, List<SaleItem> items)
        {
            if (items == null || items.Count == 0 )
            {
                Console.WriteLine("[Point of Sale]: No items to process");
                return false;
            }
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
