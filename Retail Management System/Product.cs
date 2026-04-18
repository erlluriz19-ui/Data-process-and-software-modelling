using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Product
    {

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

        public int stockQuantity
        {
            get { return _stockQuantity; }
            private set { _stockQuantity = value; }
        }

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

        public Product() { }

        public void updateStock(int quantity)
        {
            int newStock = _stockQuantity + quantity;

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