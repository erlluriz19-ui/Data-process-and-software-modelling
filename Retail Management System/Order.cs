using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents an online order placed by a Customer.
    // Composition relationship — Order ◆──── OrderItem (1 to 1..*).
    // OrderItems are owned by this Order and destroyed with it.
    // FR8, FR9
    public class Order
    {
        // Private backing fields
        private int _orderID;
        private DateTime _orderDate;
        private string _status;

        // Composition — Order owns its OrderItems
        private List<OrderItem> _items;

        public int OrderID
        {
            get { return _orderID; }
            private set { _orderID = value; }
        }

        public DateTime orderDate
        {
            get { return _orderDate; }
            private set { _orderDate = value; }
        }

        // Status values: "Pending", "Confirmed", "Cancelled", "Failed"
        public string status
        {
            get { return _status; }
            private set { _status = value; }
        }

        // Read-only external view of the items list
        public IReadOnlyList<OrderItem> Items
        {
            get { return _items.AsReadOnly(); }
        }

        // Constructor — initialises the items list as part of the composition
        public Order(int orderID, DateTime orderDate, string status)
        {
            _orderID = orderID;
            _orderDate = orderDate;
            _status = status;
            _items = new List<OrderItem>();
        }

        // Adds an OrderItem to this order (part of the Composition relationship)
        public void AddItem(OrderItem item)
        {
            if (item == null)
            {
                Console.WriteLine("[ORDER] Cannot add a null item.");
                return;
            }

            _items.Add(item);
            Console.WriteLine($"[ORDER] Item added to order #{_orderID}. " +
                              $"Total items: {_items.Count}.");
        }

        // FR8, FR9 — Confirms the order and sets status to Confirmed
        public bool placeOrder(List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("[ORDER] Cannot place an order with no products.");
                _status = "Failed";
                return false;
            }

            Console.WriteLine($"[ORDER] Confirming OrderID {_orderID} " +
                              $"with {_items.Count} line item(s)...");

            _status = "Confirmed";
            Console.WriteLine($"[ORDER] Status → {_status}");
            Console.WriteLine($"[ORDER] Order date: {_orderDate:yyyy-MM-dd HH:mm}");
            return true;
        }

        // Cancel Order extension point from the Use Case Diagram.
        // Only Pending or Confirmed orders can be cancelled.
        public void cancelOrder(int orderID)
        {
            if (_orderID != orderID)
            {
                Console.WriteLine($"[ORDER] OrderID {orderID} does not match " +
                                  $"this order (ID: {_orderID}).");
                return;
            }

            if (_status == "Pending" || _status == "Confirmed")
            {
                _status = "Cancelled";
                Console.WriteLine($"[ORDER] OrderID {_orderID} has been cancelled. " +
                                  "Status → Cancelled.");
            }
            else
            {
                // Guard: cannot cancel an already cancelled or failed order
                Console.WriteLine($"[ORDER] Cannot cancel order — " +
                                  $"current status is '{_status}'.");
            }
        }

        // Returns the sum of all OrderItem subtotals
        public double getTotal()
        {
            double total = 0;
            foreach (OrderItem item in _items)
                total += item.subtotal;
            return Math.Round(total, 2);
        }

        public override string ToString()
        {
            return $"Order[ID={_orderID}, Date={_orderDate:yyyy-MM-dd}, " +
                   $"Status={_status}, Items={_items.Count}, Total=${getTotal():F2}]";
        }
    }
}