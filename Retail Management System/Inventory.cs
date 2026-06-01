using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class Inventory
    {
        private List<IStockObserver> observers = new List<IStockObserver>();
        public List<Product> products = new List<Product>();
        private const int StockThreshold = 10;

        // Observer Pattern: attach observer
        public void attach(IStockObserver o)
        {
            observers.Add(o);
            Console.WriteLine($"  [INVENTORY] Observer '{o.GetType().Name}' registered.");
        }

        // Observer Pattern: detach observer
        public void detach(IStockObserver o)
        {
            observers.Remove(o);
            Console.WriteLine($"  [INVENTORY] Observer '{o.GetType().Name}' removed.");
        }

        // Observer Pattern: notify all observers
        public void notify(Product product)
        {
            foreach (var obs in observers)
                obs.update(product);
        }

        // FR16: Check if product is below threshold and notify
        public void checkStockThreshold(Product product)
        {
            if (product.stockQuantity < StockThreshold)
            {
                Console.WriteLine($"  [INVENTORY] WARNING: '{product.name}' below threshold ({StockThreshold}).");
                notify(product);
            }
        }

        // FR15: Find product by ID
        public Product findProduct(int productID)
        {
            return products.Find(p => p.productID == productID);
        }

        // FR6: Search products by keyword
        public void searchProducts(string keyword)
        {
            bool found = false;
            foreach (Product p in products)
            {
                if (p.name.ToLower().Contains(keyword.ToLower()))
                {
                    Console.WriteLine(p.ToString());
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine($"  [INVENTORY] No products found matching '{keyword}'.");
        }

        // FR15: Display all products
        public void displayAll()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("  [INVENTORY] No products in stock.");
                return;
            }
            Console.WriteLine($"  {"ID",-6}   {"Name",-35}   {"Price",8}   Stock");
            Console.WriteLine("  " + new string(' ', 62));
            foreach (Product p in products)
                Console.WriteLine(p.ToString());
        }

        public void addProduct(Product p)
        {
            products.Add(p);
        }
    }
}
