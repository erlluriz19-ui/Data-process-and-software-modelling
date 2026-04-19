using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents a single product in the RMS catalogue.
    // Demonstrates encapsulation — all fields are private, stock can only
    // be changed through updateStock() which validates the new value.
    // FR2, FR5, FR15
    public class Product
    {
        // Private backing fields — cannot be set directly from outside
        private int _productID;
        private string _name;
        private double _price;
        private int _stockQuantity;

        public int productID
        {
            get { return _productID; }
            private set { _productID = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Price setter rejects negative values (encapsulation — controlled mutation)
        public double price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("[PRODUCT] Price cannot be negative — update rejected.");
                    return;
                }
                _price = value;
            }
        }

        // Stock is read-only externally — only updateStock() can change it
        public int stockQuantity
        {
            get { return _stockQuantity; }
            private set { _stockQuantity = value; }
        }

        // Full constructor — validates price and stock before assigning
        public Product(int productID, string name, double price, int stockQuantity)
        {
            if (price < 0)
                throw new ArgumentException("Product price cannot be negative.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));

            _productID = productID;
            _name = name;
            _price = price;
            _stockQuantity = stockQuantity;
        }

        // Parameterless constructor kept for object initialiser compatibility
        public Product() { }

        // FR5, FR14 — Adjusts stock by the given delta (positive = add, negative = remove).
        // Guards against stock going below zero — the only safe way to change stock.
        public void updateStock(int quantity)
        {
            int newStock = _stockQuantity + quantity;

            // Guard: stock cannot go below zero
            if (newStock < 0)
            {
                Console.WriteLine($"[PRODUCT] Cannot reduce stock for '{_name}' below zero. " +
                                  $"Current: {_stockQuantity}, Requested change: {quantity}.");
                return;
            }

            int oldStock = _stockQuantity;
            _stockQuantity = newStock;

            Console.WriteLine($"[PRODUCT] Stock updated for '{_name}': " +
                              $"{oldStock} → {_stockQuantity} unit(s).");
        }

        // FR15 — Returns true if at least one unit is available
        public bool isInStock()
        {
            return _stockQuantity > 0;
        }

        public override string ToString()
        {
            return $"Product[ID={_productID}, Name={_name}, " +
                   $"Price=${_price:F2}, Stock={_stockQuantity}]";
        }
    }
}