using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class WarehouseStaff : User
    {
        private int _employeeID;
        private const int StockAlertThreshold = 5;

        public int EmployeeID
        {
            get { return _employeeID; }
            private set { _employeeID = value; }
        }

        public WarehouseStaff(int userId, string name, string email,
                              string password, int employeeID)
            : base(userId, name, email, password)
        {
            _employeeID = employeeID;
        }

        public void updateStock(int productID, int quantity, Inventory inventory)
        {
            if (quantity == 0)
            {
                Console.WriteLine("[INVENTORY] Update ignored: quantity change is zero.");
                return;
            }

            Console.WriteLine($"[INVENTORY] Updating stock for ProductID {productID} " +
                              $"by {(quantity > 0 ? "+" : "")}{quantity} unit(s)...");

            inventory.updateStock(productID, quantity);

            Console.WriteLine($"[INVENTORY] Stock update applied for ProductID {productID}.");

            checkStockAlert(productID, inventory);
        }

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
