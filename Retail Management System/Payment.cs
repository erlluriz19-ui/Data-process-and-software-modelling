using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Payment
    {
        public int paymentID;
        public double amount;
        public string status;

        public bool validatePayment(string paymentDetails)
        {
            Console.WriteLine("[PAYMENT] Validating payment details...");

            return !string.IsNullOrWhiteSpace(paymentDetails);
        }

        public bool processPayment()
        {
            Console.WriteLine($"[PAYMENT] Processing {amount:F2} via gateway [TLS 1.3]...");
            validatePayment("details");

            bool approved = amount <= 1000;

            status = approved ? "Success" : "Failed";
            Console.WriteLine($"[PAYMENT] Gateway: {(approved ? "Approved" : "Declined")}. Status - {status}.");

            if (!approved)
            {
                Console.WriteLine("[PAYMENT] Customer notified. Retry window: 24 hours.");
            }
            return approved;
        }
    }
}
