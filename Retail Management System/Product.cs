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

        //initializes product fields when object created
        public Product(int productID, string name, double price, int stockQuantity)
        {
            this.productID = productID;
            this.name = name;
            this.price = price;
            this.stockQuantity = stockQuantity;
        }

        //stops stock from going negative and updates stock quantity (business rule)
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
        //returns formatted string of all fields and is automaticall called when product object is printed to console
        public override string ToString()
        {
            return $"[ProductID: {productID}, Name: {name}, Price: ${price:F2}, Stock: {stockQuantity}]";
        }
    }
}
