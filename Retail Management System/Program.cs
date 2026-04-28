using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    internal class Program
    {
        static SalesAssociate sarah;
        static Sale demoSale;
        static Payment demoPayment;
        static Payment demoPaymentFail;
        static Product toaster;
        static Product butter;



        static void Main(string[] args)
        {
            Setup();
            ShowMenu();
        }

        static void Setup()
        {
            sarah = new SalesAssociate(194, "Sarah Smith", "sarah@twg.co.nz", "pass123", 5001);

            toaster = new Product(3341, "Kambrook 2-slice Toaster", 29.99, 14);
            butter = new Product(3342, "Anchor Butter 500g", 6.49, 22);

            demoSale = new Sale { saleID = 391, date = DateTime.Now };

            demoPayment = new Payment { paymentID = 8492, amount = 49.42 };

            demoPaymentFail = new Payment { paymentID = 8493, amount = 1200.00 };

        }

        static void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("   Retail Management System - The Warehouse Group");
                Console.WriteLine("   Phase 1 Prototype");
                Console.WriteLine("  [1]  Login / Logout         (FR20)");
                Console.WriteLine("  [2]  Process In-Store Sale  (FR1-5)");
                Console.WriteLine("  [3]  Process Payment        (FR10-13)");
                Console.WriteLine("  [0]  Exit");
                Console.Write("\n  Select an option: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": DemoLogin(); break;
                    case "2": DemoProcessSale(); break;
                    case "3": DemoPayment(); break;
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
            Console.WriteLine("__________________________________________________");
            Console.WriteLine($"  {title}");
            Console.WriteLine($"  {fr}");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine();
        }

        static void DemoLogin()
        {
            Header("USER AUTHENTICATION", "FR20 - Role-Based Access Control");
            sarah.login("sarah@twg.co.nz", "pass123");
            Console.WriteLine();
            sarah.login("sarah@twg.co.nz", "wrongpassword");
            Console.WriteLine();
            sarah.logout();
        }

        static void DemoProcessSale()
        {
            Header("PROCESS IN-STORE SALE", "FR1 FR2 FR3 FR4 FR5 - Point of Sale");

            SaleItem si1 = new SaleItem();
            si1.quantity = 1;
            si1.calculateSubtotal(toaster.price, 1);

            SaleItem si2 = new SaleItem();
            si2.quantity = 2;
            si2.calculateSubtotal(butter.price, 2);

            demoSale.items.Clear();
            sarah.processSale(demoSale, demoPayment, new List<SaleItem> { si1, si2 });

            Console.WriteLine();
            toaster.updateStock(-1);
            butter.updateStock(-2);
        }

        static void DemoPayment()
        {
            Header("PAYMENT PROCESSING", "FR10 FR11 FR12 FR13 - Secure Payments");
            demoPayment.amount = 49.42;
            demoPayment.processPayment();
            Console.WriteLine();
            demoPaymentFail.processPayment();
        }
    }
}