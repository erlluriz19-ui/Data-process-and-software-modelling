using System;

namespace Retail_Management_System
{
    // Observer Pattern - Interface (FR16: low stock alerts)
    public interface IStockObserver
    {
        void update(Product product);
    }

    // Concrete Observer 1: notifies supply chain to reorder
    public class SupplyChainObserver : IStockObserver
    {
        public void update(Product product)
        {
            Console.WriteLine($"  [SUPPLY CHAIN] Reorder triggered for '{product.name}' " +
                              $"(ID: {product.productID}). Current stock: {product.stockQuantity}");
        }
    }

    // Concrete Observer 2: refreshes manager dashboard display
    public class ManagerDashboard : IStockObserver
    {
        public void update(Product product)
        {
            Console.WriteLine($"  [MANAGER DASHBOARD] Low stock alert: '{product.name}' " +
                              $"- only {product.stockQuantity} units remaining.");
        }
    }
}
