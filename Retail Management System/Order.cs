using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Order
    {

        private int _orderID;
        private DateTime _orderDate;
        private string _status;

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

        public string status
        {
            get { return _status; }
            private set { _status = value; }
        }

        public IReadOnlyList<OrderItem> Items
        {
            get { return _items.AsReadOnly(); }
        }

        public Order(int orderID, DateTime orderDate, string status)
        {
            _orderID = orderID;
            _orderDate = orderDate;
            _status = status;
            _items = new List<OrderItem>();
        }

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
                Console.WriteLine($"[ORDER] Cannot cancel order — " +
                                  $"current status is '{_status}'.");
            }
        }

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