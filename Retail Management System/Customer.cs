using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Customer : User
    {
        private string _address;
        private string _phoneNumber;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public Customer(int userId, string name, string email, string password,
                        string address, string phoneNumber)
            : base(userId, name, email, password)
        {
            _address = address;
            _phoneNumber = phoneNumber;
        }

        public List<Product> browseProducts(string keyword, List<Product> catalogue)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("[BROWSE] Please enter a search keyword.");
                return new List<Product>();
            }

            Console.WriteLine($"[BROWSE] Searching catalogue for: \"{keyword}\"...");

            List<Product> results = catalogue
                .Where(p => p.name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            Console.WriteLine($"[BROWSE] {results.Count} result(s) found for \"{keyword}\".");
            return results;
        }

        public bool placeOrder(Order order, List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("[ORDER] Cannot place an empty order. " +
                                  "Please add products to your cart first.");
                return false;
            }

            Console.WriteLine($"[ORDER] Placing order for {products.Count} product(s)...");
            Console.WriteLine($"[ORDER] Delivery address confirmed: {_address}");
            Console.WriteLine($"[ORDER] Contact number: {_phoneNumber}");

            bool placed = order.placeOrder(products);

            if (placed)
                Console.WriteLine($"[ORDER] Order confirmed. OrderID: {order.OrderID}");
            else
                Console.WriteLine("[ORDER] Order could not be placed. Please try again.");

            return placed;
        }

        public override string ToString()
        {
            return $"Customer[ID={UserId}, Name={Name}, Phone={_phoneNumber}]";
        }
    }
}