using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Customer : User
    {
        public string address;
        public string phoneNumber;


        public List<Product> browseProducts(string keyword)
        {
            // This method would return a list of products available in the store
            return new List<Product>();
        }
       
        public bool placeOrder(List<Product> products)
        {
            // This method would handle the logic for placing an order
            return products.Count > 0;
        }
    }
}