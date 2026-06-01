using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    internal class Program
    {
        //  Shared state 
        static Inventory inventory = new Inventory();
        static List<User> users = new List<User>();

        static SalesAssociate sarah;
        static WarehouseStaff james;
        static Manager linda;
        static ITAdministrator tom;
        static Customer alice;

        static void Main(string[] args)
        {
            Seed();
            MainMenu();
        }

        //  Seed data 
        static void Seed()
        {
            sarah = new SalesAssociate(101, "Sarah Smith", "sarah@twg.co.nz", "pass123", 5001);
            james = new WarehouseStaff(102, "James Lee", "james@twg.co.nz", "pass456", 5002);
            linda = new Manager(103, "Linda Chen", "linda@twg.co.nz", "pass789", "Retail Operations");
            tom = new ITAdministrator(104, "Tom Baker", "tom@twg.co.nz", "admin01", 3);
            alice = new Customer(201, "Alice Walker", "alice@gmail.com", "cust01",
                                        "12 Queen St, Auckland", "021-555-0199");

            users.AddRange(new User[] { sarah, james, linda, tom, alice });

            inventory.attach(new SupplyChainObserver());
            inventory.attach(new ManagerDashboard());

            inventory.addProduct(new Product(3341, "Kambrook 2-Slice Toaster", 29.99, 14));
            inventory.addProduct(new Product(3342, "Anchor Butter 500g", 6.49, 22));
            inventory.addProduct(new Product(3343, "Samsung 55\" 4K TV", 799.99, 8));
            inventory.addProduct(new Product(3344, "Breville Kettle 1.7L", 49.99, 12));
            inventory.addProduct(new Product(3345, "Glad Wrap 30m", 4.99, 35));
            inventory.addProduct(new Product(3346, "Philips Air Fryer 4.1L", 119.99, 6));
        }

        //  Helpers 
        static void Header(string title, string fr = "")
        {
            Console.WriteLine();
            Console.WriteLine("  " + new string('=', 50));
            Console.WriteLine($"  {title}");
            if (!string.IsNullOrEmpty(fr))
                Console.WriteLine($"  {fr}");
            Console.WriteLine("  " + new string('=', 50));
            Console.WriteLine();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.Write("  Press any key to continue...");
            Console.ReadKey();
        }

        static string Prompt(string msg)
        {
            Console.Write($"  {msg}: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        //  Main Menu 
        static void MainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("  " + new string('=', 52));
                Console.WriteLine("  RETAIL MANAGEMENT SYSTEM");
                Console.WriteLine("  The Warehouse Group NZ");
                Console.WriteLine("  " + new string('=', 52));
                Console.WriteLine("  [1]  User Authentication        (FR20)");
                Console.WriteLine("  [2]  Process In-Store Sale      (FR1-FR5)");
                Console.WriteLine("  [3]  Payment Processing         (FR10-FR13)");
                Console.WriteLine("  [4]  Inventory Management       (FR14-FR16)");
                Console.WriteLine("  [5]  Customer Online Order      (FR6-FR9)");
                Console.WriteLine("  [6]  Reports and Export         (FR17-FR19)");
                Console.WriteLine("  [7]  IT Admin and Maintenance   (FR20/NFR)");
                Console.WriteLine("  [8]  Observer Pattern Demo");
                Console.WriteLine("  [9]  Strategy Pattern Demo");
                Console.WriteLine("  [0]  Exit");
                Console.WriteLine("  " + new string('=', 52));
                Console.Write("\n  Select option: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": MenuAuthentication(); break;
                    case "2": MenuProcessSale(); break;
                    case "3": MenuPayment(); break;
                    case "4": MenuInventory(); break;
                    case "5": MenuOnlineOrder(); break;
                    case "6": MenuReports(); break;
                    case "7": MenuITAdmin(); break;
                    case "8": MenuObserverDemo(); break;
                    case "9": MenuStrategyDemo(); break;
                    case "0": Console.WriteLine("\n  Exiting RMS. Goodbye!"); running = false; break;
                    default: Console.WriteLine("\n  Invalid option."); Pause(); break;
                }
            }
        }

        //  [1] Authentication
        static void MenuAuthentication()
        {
            Console.Clear();
            Header("USER AUTHENTICATION", "FR20 - Role-Based Access Control");
            Console.WriteLine("  Registered accounts:");
            Console.WriteLine();
            foreach (User u in users)
                Console.WriteLine($"  {u.GetType().Name,-20}  Email: {u.email}");
            Console.WriteLine();

            bool back = false;
            while (!back)
            {
                Console.WriteLine("  [1]  Login");
                Console.WriteLine("  [2]  Logout");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        string email = Prompt("Email");
                        string pass = Prompt("Password");
                        User found = users.Find(u => u.email == email);
                        if (found != null)
                            found.login(email, pass);
                        else
                            Console.WriteLine("  [AUTH] No account found with that email.");
                        Console.WriteLine();
                        break;
                    case "2":
                        string logoutEmail = Prompt("Email of user to logout");
                        User logoutUser = users.Find(u => u.email == logoutEmail);
                        if (logoutUser != null)
                            logoutUser.logout();
                        else
                            Console.WriteLine("  [AUTH] No account found with that email.");
                        Console.WriteLine();
                        break;
                    case "0": back = true; break;
                    default: Console.WriteLine("  Invalid."); Console.WriteLine(); break;
                }
            }
        }

        //  [2] Process Sale 
        static void MenuProcessSale()
        {
            Console.Clear();
            Header("PROCESS IN-STORE SALE", "FR1 FR2 FR3 FR4 FR5 - Point of Sale");

            inventory.displayAll();
            Console.WriteLine();

            List<SaleItem> items = new List<SaleItem>();
            bool adding = true;

            while (adding)
            {
                string idStr = Prompt("Enter Product ID to add (or 0 to finish)");
                if (idStr == "0") { adding = false; break; }

                if (!int.TryParse(idStr, out int pid))
                { Console.WriteLine("  Invalid ID."); continue; }

                Product p = inventory.findProduct(pid);
                if (p == null)
                { Console.WriteLine("  Product not found."); continue; }

                string qtyStr = Prompt($"Quantity for '{p.name}'");
                if (!int.TryParse(qtyStr, out int qty) || qty <= 0)
                { Console.WriteLine("  Invalid quantity."); continue; }

                if (qty > p.stockQuantity)
                { Console.WriteLine($"  Insufficient stock. Available: {p.stockQuantity}"); continue; }

                SaleItem si = new SaleItem();
                si.quantity = qty;
                si.calculateSubtotal(p.price, qty);
                items.Add(si);

                // FR4: Update stock in real time
                p.updateStock(-qty);
                inventory.checkStockThreshold(p);
            }

            if (items.Count == 0)
            { Console.WriteLine("  No items added. Returning to menu."); Pause(); return; }

            Sale sale = new Sale { saleID = new Random().Next(1000, 9999), date = DateTime.Now };
            Payment payment = new Payment { paymentID = new Random().Next(8000, 8999) };

            Console.WriteLine();
            Console.WriteLine("  Select payment method:");
            Console.WriteLine("  [1] Cash  [2] Card  [3] Online");
            Console.Write("  Choice: ");
            IPaymentStrategy strategy;
            switch (Console.ReadLine()?.Trim())
            {
                case "2": strategy = new CardPayment(); break;
                case "3": strategy = new OnlinePayment(); break;
                default: strategy = new CashPayment(); break;
            }

            Console.WriteLine();
            double total = sale.calculateTotal() == 0
                ? items[0].subtotal // fallback for manual items
                : sale.calculateTotal();

            // recalculate via processSale
            sarah.processSale(sale, payment, items);
            Pause();
        }

        //  [3] Payment Processing 
        static void MenuPayment()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Header("PAYMENT PROCESSING", "FR10 FR11 FR12 FR13 - Secure Payments");
                Console.WriteLine("  [1]  Cash payment");
                Console.WriteLine("  [2]  Card payment (approve)");
                Console.WriteLine("  [3]  Card payment (decline - over limit)");
                Console.WriteLine("  [4]  Online payment");
                Console.WriteLine("  [5]  Custom amount");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": RunPayment(49.99, new CashPayment()); break;
                    case "2": RunPayment(49.99, new CardPayment()); break;
                    case "3": RunPayment(1500.00, new CardPayment()); break;
                    case "4": RunPayment(349.00, new OnlinePayment()); break;
                    case "5":
                        string amt = Prompt("Enter amount");
                        if (double.TryParse(amt, out double a))
                        {
                            Console.WriteLine("  [1] Cash  [2] Card  [3] Online");
                            Console.Write("  Method: ");
                            IPaymentStrategy s;
                            switch (Console.ReadLine()?.Trim())
                            {
                                case "2": s = new CardPayment(); break;
                                case "3": s = new OnlinePayment(); break;
                                default: s = new CashPayment(); break;
                            }
                            RunPayment(a, s);
                        }
                        break;
                    case "0": back = true; break;
                    default: Console.WriteLine("  Invalid."); Pause(); break;
                }
            }
        }

        static void RunPayment(double amount, IPaymentStrategy strategy)
        {
            Console.WriteLine();
            Payment p = new Payment { paymentID = new Random().Next(8000, 8999), amount = amount };
            p.processPayment(amount, strategy);
            Pause();
        }

        //  [4] Inventory Management 
        static void MenuInventory()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Header("INVENTORY MANAGEMENT", "FR14 FR15 FR16 - Stock Control");
                Console.WriteLine("  [1]  View all inventory");
                Console.WriteLine("  [2]  Search product by keyword");
                Console.WriteLine("  [3]  Update stock (Warehouse Staff)");
                Console.WriteLine("  [4]  Check low stock threshold");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        Console.Clear();
                        Header("VIEW INVENTORY", "FR15");
                        james.viewInventory(inventory);
                        Pause();
                        break;
                    case "2":
                        Console.Clear();
                        Header("SEARCH PRODUCTS", "FR6 / FR15");
                        string kw = Prompt("Enter keyword");
                        alice.browseProducts(kw, inventory);
                        Pause();
                        break;
                    case "3":
                        Console.Clear();
                        Header("UPDATE STOCK", "FR14");
                        inventory.displayAll();
                        Console.WriteLine();
                        string pidStr = Prompt("Product ID");
                        string qtyStr = Prompt("Quantity change (e.g. -5 to reduce, +10 to add)");
                        if (int.TryParse(pidStr, out int pid) && int.TryParse(qtyStr, out int qty))
                            james.updateStock(pid, qty, inventory);
                        else
                            Console.WriteLine("  Invalid input.");
                        Pause();
                        break;
                    case "4":
                        Console.Clear();
                        Header("LOW STOCK CHECK", "FR16");
                        foreach (Product p in inventory.products)
                            inventory.checkStockThreshold(p);
                        Pause();
                        break;
                    case "0": back = true; break;
                    default: Console.WriteLine("  Invalid."); Pause(); break;
                }
            }
        }

        //  [5] Online Order 
        static void MenuOnlineOrder()
        {
            Console.Clear();
            Header("CUSTOMER ONLINE ORDER", "FR6 FR7 FR8 FR9 - E-Commerce");

            Console.WriteLine($"  Logged in as: {alice.name} ({alice.email})");
            Console.WriteLine($"  Delivery address: {alice.address}");
            Console.WriteLine();

            inventory.displayAll();
            Console.WriteLine();

            List<Product> cart = new List<Product>();
            bool adding = true;

            while (adding)
            {
                string idStr = Prompt("Add Product ID to cart (or 0 to checkout)");
                if (idStr == "0") { adding = false; break; }
                if (!int.TryParse(idStr, out int pid)) { Console.WriteLine("  Invalid."); continue; }
                Product p = inventory.findProduct(pid);
                if (p == null) { Console.WriteLine("  Product not found."); continue; }
                cart.Add(p);
                Console.WriteLine($"  [CART] '{p.name}' added.");
            }

            if (cart.Count > 0)
            {
                Order order = new Order(new Random().Next(2000, 2999));
                alice.placeOrder(cart, inventory);
                order.placeOrder(cart);
                Console.WriteLine($"  [ORDER] Status: {order.status}");
            }
            else
            {
                Console.WriteLine("  Cart empty. Order cancelled.");
            }
            Pause();
        }

        //  [6] Reports 
        static void MenuReports()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Header("REPORTS & EXPORT", "FR17 FR18 FR19 - Reporting");
                Console.WriteLine("  [1]  Generate Sales Report");
                Console.WriteLine("  [2]  Generate Inventory Report");
                Console.WriteLine("  [3]  View Sales Report by Date Range");
                Console.WriteLine("  [4]  Export Report");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        Console.Clear();
                        Header("SALES REPORT", "FR17");
                        linda.generateReport("sales");
                        Pause();
                        break;
                    case "2":
                        Console.Clear();
                        Header("INVENTORY REPORT", "FR18");
                        linda.generateReport("inventory");
                        Pause();
                        break;
                    case "3":
                        Console.Clear();
                        Header("SALES REPORT BY DATE", "FR18");
                        linda.viewSalesReport(new DateTime(2026, 5, 1), DateTime.Now);
                        Pause();
                        break;
                    case "4":
                        Console.Clear();
                        Header("EXPORT REPORT", "FR19");
                        Report r = linda.generateReport("sales");
                        Console.WriteLine();
                        Console.Write("  Export format [csv/pdf/xlsx]: ");
                        string fmt = Console.ReadLine()?.Trim() ?? "csv";
                        r.exportReport(fmt);
                        Pause();
                        break;
                    case "0": back = true; break;
                    default: Console.WriteLine("  Invalid."); Pause(); break;
                }
            }
        }

        //  [7] IT Admin 
        static void MenuITAdmin()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Header("IT ADMINISTRATION", "FR20 NFR2 NFR3 NFR7 - System Management");
                Console.WriteLine("  [1]  System maintenance & diagnostics");
                Console.WriteLine("  [2]  Add user");
                Console.WriteLine("  [3]  Remove user");
                Console.WriteLine("  [4]  List all users");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        Console.Clear();
                        Header("SYSTEM MAINTENANCE", "NFR2 NFR3 NFR7");
                        tom.maintainSystem();
                        Pause();
                        break;
                    case "2":
                        Console.Clear();
                        Header("ADD USER", "FR20");
                        string newId = Prompt("New user ID");
                        if (int.TryParse(newId, out int nid))
                            tom.manageUsers(nid, "add", users);
                        Pause();
                        break;
                    case "3":
                        Console.Clear();
                        Header("REMOVE USER", "FR20");
                        Console.WriteLine("  Current users:");
                        foreach (User u in users)
                            Console.WriteLine($"  ID: {u.userID,-5} | {u.name,-20} | {u.email}");
                        Console.WriteLine();
                        string remId = Prompt("User ID to remove");
                        if (int.TryParse(remId, out int rid))
                            tom.manageUsers(rid, "remove", users);
                        Pause();
                        break;
                    case "4":
                        Console.Clear();
                        Header("ALL USERS", "FR20");
                        foreach (User u in users)
                            Console.WriteLine($"  ID: {u.userID,-5} | {u.GetType().Name,-20} | {u.name,-20} | {u.email}");
                        Pause();
                        break;
                    case "0": back = true; break;
                    default: Console.WriteLine("  Invalid."); Pause(); break;
                }
            }
        }

        //  [8] Observer Pattern Demo 
        static void MenuObserverDemo()
        {
            Console.Clear();
            Header("OBSERVER PATTERN DEMO", "GoF - Inventory Stock Notifications (FR16)");
            Console.WriteLine("  Observers attached: SupplyChainObserver, ManagerDashboard");
            Console.WriteLine("  Stock threshold: 10 units");
            Console.WriteLine();

            Console.WriteLine("  Reducing Samsung TV stock from 8 → 5 (already below threshold)...");
            Product tv = inventory.findProduct(3343);
            if (tv != null)
            {
                tv.updateStock(-3);
                inventory.checkStockThreshold(tv);
            }

            Console.WriteLine();
            Console.WriteLine("  Reducing Philips Air Fryer stock from 6 → 3...");
            Product af = inventory.findProduct(3346);
            if (af != null)
            {
                af.updateStock(-3);
                inventory.checkStockThreshold(af);
            }

            Console.WriteLine();
            Console.WriteLine("  Both observers notified automatically via notify() - no direct coupling.");
            Pause();
        }

        //  [9] Strategy Pattern Demo
        static void MenuStrategyDemo()
        {
            Console.Clear();
            Header("STRATEGY PATTERN DEMO", "GoF - Payment Strategy (FR10-FR13)");
            Console.WriteLine("  Same Payment context - strategy is selected at runtime.");
            Console.WriteLine();

            bool back = false;
            while (!back)
            {
                Console.WriteLine("  [1]  Cash payment");
                Console.WriteLine("  [2]  Card payment");
                Console.WriteLine("  [3]  Online payment");
                Console.WriteLine("  [0]  Back");
                Console.Write("\n  Select: ");

                string choice = Console.ReadLine()?.Trim();
                if (choice == "0") { back = true; break; }
                if (choice != "1" && choice != "2" && choice != "3")
                { Console.WriteLine("  Invalid."); Console.WriteLine(); continue; }

                string amtStr = Prompt("Enter amount");
                if (!double.TryParse(amtStr, out double amt))
                { Console.WriteLine("  Invalid amount."); Console.WriteLine(); continue; }

                IPaymentStrategy strategy;
                switch (choice)
                {
                    case "2": strategy = new CardPayment(); break;
                    case "3": strategy = new OnlinePayment(); break;
                    default: strategy = new CashPayment(); break;
                }

                Payment p = new Payment { paymentID = new Random().Next(9000, 9999) };
                Console.WriteLine();
                p.processPayment(amt, strategy);
                Console.WriteLine();
            }
        }
    }
}