using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Manages the collection of products in a store or warehouse.
    // Inventory holds references to Products but does not own them exclusively.
    // FR14, FR15, FR16
    public class Inventory
    {
        public int inventoryID;

        // Aggregation — list of product references (not owned, not destroyed with Inventory)
        public List<Product> products = new List<Product>();

        // Stock alert fires when a product falls below this threshold (FR16)
        private const int StockAlertThreshold = 5;

        // FR15 — Returns and displays all products with their current stock status
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

        // FR14, FR5 — Finds the product and delegates the stock change to Product.updateStock().
        // After updating, checks if a low-stock alert should fire (FR16).
        public void updateStock(int productID, int quantity)
        {
            Product product = products.FirstOrDefault(p => p.productID == productID);

            if (product == null)
            {
                Console.WriteLine($"[INVENTORY] ProductID {productID} not found " +
                                  "in this inventory.");
                return;
            }

            // Delegate to Product — keeps stock validation logic in one place (high cohesion)
            product.updateStock(quantity);

            // FR16 — Raise alert if stock has dropped below threshold
            if (product.stockQuantity < StockAlertThreshold)
            {
                Console.WriteLine($"[ALERT] *** LOW STOCK ALERT *** " +
                                  $"'{product.name}' (ProductID {productID}) " +
                                  $"has only {product.stockQuantity} unit(s) remaining. " +
                                  $"Threshold: {StockAlertThreshold}.");
                Console.WriteLine("[ALERT] Notification sent to Supply Chain & Logistics.");
            }
        }

        // Adds a new product to the inventory
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