using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Product
    {
        public int productID;
        public string name;
        public double price;
        public int stockQuantity;

        public Product(int productID, string name, double price, int stockQuantity)
        {
            this.productID = productID;
            this.name = name;
            this.price = price;
            this.stockQuantity = stockQuantity;
        }

        public void updateStock(int quantity)
        {
            if (stockQuantity + quantity < 0)
            {
                Console.WriteLine($"[PRODUCT] Cannot reduce '{name}' below zero.");
                return;
            }
            stockQuantity += quantity;
            Console.WriteLine($"[PRODUCT] '{name}' stock updated. New quantity: {stockQuantity}");
        }
        public override string ToString()
        {
            return $"[ProductID: {productID}, Name: {name}, Price: ${price:F2}, Stock: {stockQuantity}]";
        }
    }
}
