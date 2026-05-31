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
        //validates payment details before doing anything
        //by checking if the string is null, empty or whitespace
        public bool validatePayment(string paymentDetails)
        {
            Console.WriteLine("[PAYMENT] Validating payment details...");

            return !string.IsNullOrWhiteSpace(paymentDetails);
        }

        public bool processPayment()
        {  //encrypts data
            Console.WriteLine($"[PAYMENT] Processing ${amount:F2} via gateway [TLS 1.3]...");
            validatePayment("details");

            //payment is declined if amount exceeds 1000 (business rule)
            //notifies customer after
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

    public class CardPayment : IPaymentStrategy
    {
        public bool Pay(double amount)
        {
            Console.WriteLine($"[CARD] Processing ${amount:F2} via card gateway [TLS 1.3]...");
            bool approved = amount <= 1000;
            Console.WriteLine($"[CARD] {(approved ? "APPROVED" : "DECLINED")}");
            return approved;
        }
    }

    public class CashPayment : IPaymentStrategy
    {
        public bool Pay(double amount)
        {
            Console.WriteLine($"[CASH] Received ${amount:F2} in cash. Payment accepted.");
            return true;
        }
    }

    public class OnlinePayment : IPaymentStrategy
    {
        public bool Pay(double amount)
        {
            Console.WriteLine($"[ONLINE] Processing ${amount:F2} via online gateway...");
            bool approved = amount <= 2000;
            Console.WriteLine($"[ONLINE] {(approved ? "APPROVED" : "DECLINED")}");
            return approved;
        }
    }


}
