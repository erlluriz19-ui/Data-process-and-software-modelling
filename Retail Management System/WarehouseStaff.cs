using System;

namespace Retail_Management_System
{
    public class WarehouseStaff : User
    {
        public int employeeID;

        public WarehouseStaff(int userID, string name, string email, string password, int employeeID)
            : base(userID, name, email, password)
        {
            this.employeeID = employeeID;
        }

        // FR14: Update stock quantity for a product
        public void updateStock(int productID, int quantity, Inventory inventory)
        {
            Product p = inventory.findProduct(productID);
            if (p == null)
            {
                Console.WriteLine($"  [WAREHOUSE] Product ID {productID} not found.");
                return;
            }
            p.updateStock(quantity);
            inventory.checkStockThreshold(p);
        }

        // FR15: View full inventory
        public void viewInventory(Inventory inventory)
        {
            Console.WriteLine("  [WAREHOUSE] Current Inventory:");
            inventory.displayAll();
        }
    }
}
