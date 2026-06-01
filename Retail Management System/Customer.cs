using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class Customer : User
    {
        public string address;
        public string phoneNumber;

        public Customer(int userID, string name, string email, string password, string address, string phoneNumber)
            : base(userID, name, email, password)
        {
            this.address = address;
            this.phoneNumber = phoneNumber;
        }

        // FR6: Browse products by keyword
        public void browseProducts(string keyword, Inventory inventory)
        {
            Console.WriteLine($"  [CUSTOMER] Searching for: \"{keyword}\"");
            inventory.searchProducts(keyword);
        }

        // FR7/FR8/FR9: Place an online order
        public bool placeOrder(List<Product> products, Inventory inventory)
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("  [CUSTOMER] Cart is empty.");
                return false;
            }
            Console.WriteLine($"  [CUSTOMER] Placing order for {products.Count} product(s)...");
            Console.WriteLine($"  [CUSTOMER] Delivery address: {address}");
            Console.WriteLine("  [CUSTOMER] Order confirmed. Estimated delivery: 3-5 business days.");
            return true;
        }
    }
}
