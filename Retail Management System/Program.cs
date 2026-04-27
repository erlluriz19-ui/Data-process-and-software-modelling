using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using Retail_Management_System.Classes;

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

        static void setup ()
        {
            sarah = new SalesAssociate(194, "Sarah Smith", "sarah@twg.co.nz", "pass123", 5001);

            toaster = new Product(3341, "Kambrook 2-slice Toaster", 29.99, 14);
            butter = new Product(3342, "Anchor Butter 500g", 6.49, 22);

            demoSale = new Sale { saleID = 391, date = DateTime.Now };

            demoPayment = new Payment { paymentID = 8492, amount = 49.42 };

            demoPaymentFail = new Payment { paymentID = 8493, amount = 1200.00 };

        }

        static void main(string[] args)
        {
            setup();
        }
    }
}