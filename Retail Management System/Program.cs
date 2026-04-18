using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    internal class Program
    {
        static Inventory inventory;
        static SalesAssociate sarah;
        static WarehouseStaff james;
        static Manager anna;
        static ITAdministrator adminUser;
        static Customer liam;
        static Sale demoSale;
        static Payment demoPayment;
        static Payment demoPaymentFail;
        static Order demoOrder;
        static OrderItem demoOrderItem;
        static Report demoSalesReport;
        static Report demoInventoryReport;

        static void Main(string[] args)
        {
            Setup();
            ShowMenu();
        }

        static void Setup()
        {
            inventory = new Inventory { inventoryID = 1 };
            inventory.addProduct(new Product(3341, "Kambrook 2-Slice Toaster", 29.99, 14));
            inventory.addProduct(new Product(887, "Anchor Butter 500g", 6.49, 22));
            inventory.addProduct(new Product(2210, "Samsung 65-inch TV", 899.00, 3));
            inventory.addProduct(new Product(41, "A4 Copy Paper Ream", 8.99, 1));
            inventory.addProduct(new Product(1129, "Nespresso Vertuo Pop", 149.00, 18));

            sarah = new SalesAssociate(194, "Sarah Tane", "sarah@twg.co.nz", "pass123", 5001);
            james = new WarehouseStaff(200, "James Park", "james@twg.co.nz", "wh456", 6001);
            anna = new Manager(12, "Anna Reid", "anna@twg.co.nz", "mgr789", "Retail Operations");
            adminUser = new ITAdministrator(1, "Te Koha Reweti", "tkoha@twg.co.nz", "admin999", 2);
            liam = new Customer(571, "Liam Ngata", "liam@gmail.com", "secure456",
                                           "12 Pakuranga Rd, Auckland", "021-555-0199");

            demoSale = new Sale { saleID = 391, date = DateTime.Now };
            demoPayment = new Payment { paymentID = 8492, amount = 49.42 };
            demoPaymentFail = new Payment { paymentID = 8493, amount = 1200.00 };

            demoOrderItem = new OrderItem();
            demoOrderItem.quantity = 1;
            demoOrderItem.calculateSubtotal(149.00, 1);

            demoOrder = new Order(2281, DateTime.Now, "Pending");
            demoOrder.AddItem(demoOrderItem);

            demoSalesReport = new Report { reportID = 1001, type = "sales" };
            demoInventoryReport = new Report { reportID = 1002, type = "inventory" };
        }

        static void ShowMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("   Retail Management System — The Warehouse Group  ");
                Console.WriteLine("   Phase 1 Demo                                    ");
                Console.WriteLine("  [1]  Login / Logout             (FR20)           ");
                Console.WriteLine("  [2]  Process In-Store Sale      (FR1-5)          ");
                Console.WriteLine("  [3]  View Inventory             (FR15)           ");
                Console.WriteLine("  [4]  Update Stock               (FR14, FR16)     ");
                Console.WriteLine("  [5]  Browse Products Online     (FR6)            ");
                Console.WriteLine("  [6]  Place Online Order         (FR7-9)          ");
                Console.WriteLine("  [7]  Process Payment            (FR10-13)        ");
                Console.WriteLine("  [8]  Generate and Export Report (FR17-19)        ");
                Console.WriteLine("  [9]  Manage Users               (FR20)           ");
                Console.WriteLine("  [10] System Maintenance         (NFR2, 3, 7)     ");
                Console.WriteLine("  [0]  Exit                                        ");
                Console.Write("\n  Select an option: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": DemoLogin(); break;
                    case "2": DemoProcessSale(); break;
                    case "3": DemoViewInventory(); break;
                    case "4": DemoUpdateStock(); break;
                    case "5": DemoBrowseProducts(); break;
                    case "6": DemoPlaceOrder(); break;
                    case "7": DemoPayment(); break;
                    case "8": DemoReports(); break;
                    case "9": DemoManageUsers(); break;
                    case "10": DemoMaintainSystem(); break;
                    case "0":
                        Console.WriteLine("  Exiting RMS. Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("  Invalid option. Please try again.");
                        break;
                }

                if (running && input != "0")
                {
                    Console.WriteLine();
                    Console.Write("  Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }

        static void Header(string title, string fr)
        {
            Console.WriteLine("══════════════════════════════════════════════════");
            Console.WriteLine($"  {title}");
            Console.WriteLine($"  {fr}");
            Console.WriteLine("══════════════════════════════════════════════════");
            Console.WriteLine();
        }

        static void DemoLogin()
        {
            Header("USER AUTHENTICATION", "FR20 — Role-Based Access Control");

            sarah.login("sarah@twg.co.nz", "pass123");
            Console.WriteLine();
            liam.login("liam@gmail.com", "wrongpassword");
            Console.WriteLine();
            liam.login("liam@gmail.com", "secure456");
            Console.WriteLine();
            sarah.logout();
        }

        static void DemoProcessSale()
        {
            Header("PROCESS IN-STORE SALE", "FR1 FR2 FR3 FR4 FR5 — Point of Sale");

            Product toaster = inventory.products.First(p => p.productID == 3341);
            Product butter = inventory.products.First(p => p.productID == 887);

            SaleItem si1 = new SaleItem();
            si1.quantity = 1;
            si1.calculateSubtotal(toaster.price, 1);

            SaleItem si2 = new SaleItem();
            si2.quantity = 2;
            si2.calculateSubtotal(butter.price, 2);

            demoSale.items.Clear();
            demoPayment.amount = si1.subtotal + si2.subtotal;

            sarah.processSale(demoSale, demoPayment, new List<SaleItem> { si1, si2 });
            Console.WriteLine();
            toaster.updateStock(-1);
            butter.updateStock(-2);
        }

        static void DemoViewInventory()
        {
            Header("VIEW INVENTORY", "FR15 — Check Stock Availability");

            james.viewInventory(inventory);
        }

        static void DemoUpdateStock()
        {
            Header("UPDATE STOCK", "FR14 FR16 — Update Stock and Stock Alerts");

            james.updateStock(41, 50, inventory);
            Console.WriteLine();
            james.updateStock(2210, -2, inventory);
        }

        static void DemoBrowseProducts()
        {
            Header("BROWSE PRODUCTS ONLINE", "FR6 — Customer Product Search");

            List<Product> results = liam.browseProducts("butter", inventory.products);
            foreach (Product p in results)
                Console.WriteLine($"  Found: {p}");

            Console.WriteLine();
            liam.browseProducts("", inventory.products);
        }

        static void DemoPlaceOrder()
        {
            Header("PLACE ONLINE ORDER", "FR7 FR8 FR9 — Cart and Checkout");

            List<Product> cart = new List<Product>
            {
                inventory.products.First(p => p.productID == 1129)
            };

            liam.placeOrder(demoOrder, cart);
            Console.WriteLine();
            demoOrder.cancelOrder(2281);
            Console.WriteLine();
            demoOrder.cancelOrder(2281);
        }

        static void DemoPayment()
        {
            Header("PAYMENT PROCESSING", "FR10 FR11 FR12 FR13 — Secure Payments");

            demoPayment.amount = 49.42;
            demoPayment.processPayment();
            Console.WriteLine();
            demoPaymentFail.processPayment();
        }

        static void DemoReports()
        {
            Header("REPORTS", "FR17 FR18 FR19 — Generate and Export Reports");

            anna.generateReport("sales", demoSalesReport);
            Console.WriteLine();
            anna.generateReport("inventory", demoInventoryReport);
            Console.WriteLine();
            anna.viewSalesReport(new DateTime(2024, 4, 1), new DateTime(2024, 4, 17));
            Console.WriteLine();
            demoSalesReport.exportReport("xlsx");
            Console.WriteLine();
            demoInventoryReport.exportReport("pdf");
        }

        static void DemoManageUsers()
        {
            Header("MANAGE USER ACCOUNTS", "FR20 — IT Administrator RBAC");

            adminUser.manageUsers(203);
            Console.WriteLine();
            adminUser.manageUsers(-1);
        }

        static void DemoMaintainSystem()
        {
            Header("SYSTEM MAINTENANCE", "NFR2 NFR3 NFR7 — Availability, Security, Compliance");

            adminUser.maintainSystem();
        }
    }
}