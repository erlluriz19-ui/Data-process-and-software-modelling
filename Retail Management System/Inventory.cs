using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Inventory
    {

        public int inventoryID;

        public List<Product> products = new List<Product>();

        private const int StockAlertThreshold = 5;

        public List<Product> viewInventory()
        {
            Console.WriteLine($"[INVENTORY] Inventory #{inventoryID} — " +
                              $"{products.Count} product(s) on record.");

            foreach (Product product in products)
            {
                string status = product.stockQuantity == 0
                                ? "OUT OF STOCK"
                                : product.stockQuantity < StockAlertThreshold
                                    ? "LOW STOCK"
                                    : "OK";

                Console.WriteLine($"   {product} — Status: {status}");
            }

            return products;
        }

        public void updateStock(int productID, int quantity)
        {

            Product product = products.FirstOrDefault(p => p.productID == productID);

            if (product == null)
            {
                Console.WriteLine($"[INVENTORY] ProductID {productID} not found " +
                                  "in this inventory.");
                return;
            }

            product.updateStock(quantity);

            if (product.stockQuantity < StockAlertThreshold)
            {
                Console.WriteLine($"[ALERT] *** LOW STOCK ALERT *** " +
                                  $"'{product.name}' (ProductID {productID}) " +
                                  $"has only {product.stockQuantity} unit(s) remaining. " +
                                  $"Threshold: {StockAlertThreshold}.");
                Console.WriteLine("[ALERT] Notification sent to Supply Chain & Logistics.");
            }
        }

        public void addProduct(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("[INVENTORY] Cannot add a null product.");
                return;
            }

            products.Add(product);
            Console.WriteLine($"[INVENTORY] Product added: {product.name} " +
                              $"(ProductID {product.productID}).");
        }

        public override string ToString()
        {
            return $"Inventory[ID={inventoryID}, Products={products.Count}]";
        }
    }
}