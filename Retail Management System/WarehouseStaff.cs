using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents a Warehouse Staff member who manages physical stock levels.
    // Extends User (Inheritance) — reuses login, logout, and shared account fields.
    // FR14, FR15, FR16
    public class WarehouseStaff : User
    {
        private int _employeeID;

        // Alert fires when stock falls below this number (FR16)
        private const int StockAlertThreshold = 5;

        public int EmployeeID
        {
            get { return _employeeID; }
            private set { _employeeID = value; }
        }

        // Calls base(...) to initialise inherited User fields
        public WarehouseStaff(int userId, string name, string email,
                              string password, int employeeID)
            : base(userId, name, email, password)
        {
            _employeeID = employeeID;
        }

        // FR14, FR16 — Updates stock for a product and checks for low-stock alert.
        // Inventory is passed in from Program.cs (low coupling — not created here).
        public void updateStock(int productID, int quantity, Inventory inventory)
        {
            if (quantity == 0)
            {
                Console.WriteLine("[INVENTORY] Update ignored: quantity change is zero.");
                return;
            }

            Console.WriteLine($"[INVENTORY] Updating stock for ProductID {productID} " +
                              $"by {(quantity > 0 ? "+" : "")}{quantity} unit(s)...");

            // Delegate the actual stock change to Inventory, which calls Product.updateStock()
            inventory.updateStock(productID, quantity);

            Console.WriteLine($"[INVENTORY] Stock update applied for ProductID {productID}.");

            // FR16 — Check if stock has dropped below the alert threshold
            checkStockAlert(productID, inventory);
        }

        // FR15 — Displays all products and their current stock status.
        // Inventory is passed in from Program.cs (low coupling).
        public void viewInventory(Inventory inventory)
        {
            Console.WriteLine("[INVENTORY] Loading current inventory snapshot...");

            List<Product> products = inventory.viewInventory();

            if (products.Count == 0)
            {
                Console.WriteLine("[INVENTORY] No products found in inventory.");
                return;
            }

            Console.WriteLine($"{"ProductID",-12} {"Name",-30} {"Stock",8} {"Status",10}");
            Console.WriteLine(new string('-', 64));

            foreach (Product product in products)
            {
                // Determine stock status for display
                string status = product.stockQuantity > StockAlertThreshold
                                ? "OK"
                                : product.stockQuantity == 0
                                    ? "OUT OF STOCK"
                                    : "LOW";

                Console.WriteLine($"{product.productID,-12} {product.name,-30} " +
                                  $"{product.stockQuantity,8} {status,10}");
            }

            Console.WriteLine(new string('-', 64));
            Console.WriteLine($"[INVENTORY] {products.Count} product(s) listed.");
        }

        // FR16 — Private helper that fires a stock alert if quantity is below threshold.
        // Called automatically by updateStock() after every stock change.
        private void checkStockAlert(int productID, Inventory inventory)
        {
            Product product = inventory.products
                                       .FirstOrDefault(p => p.productID == productID);

            if (product == null)
                return;

            if (product.stockQuantity < StockAlertThreshold)
            {
                Console.WriteLine($"[ALERT] *** STOCK ALERT *** ProductID {productID} " +
                                  $"'{product.name}' has only {product.stockQuantity} unit(s) " +
                                  $"remaining (threshold: {StockAlertThreshold}).");
                Console.WriteLine("[ALERT] Notification dispatched to Supply Chain team.");
            }
        }

        public override string ToString()
        {
            return $"WarehouseStaff[EmployeeID={_employeeID}, Name={Name}]";
        }
    }
}