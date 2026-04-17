using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
     public class SalesAssociate : User
    {
        public int employeeID;

        public bool processSale(List<SaleItem> items)
        {
            // This method would handle the logic for processing a sale
            return items.Count > 0;
        }

        publiclic void generateReceipt(int saleID)
        {
            // This method would generate a receipt for the given sale ID
        }
    }
}
