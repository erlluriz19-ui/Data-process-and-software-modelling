using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class WarehouseStaff : User
    {
        public int employeeID;

        public void updateStock(int productID, int quantity)
        {
            // This method would handle the logic for updating stock levels for a given product
        }

        public void viewInventory()
        {
            // This method would return a list of products and their current stock levels
        }
    }
}
