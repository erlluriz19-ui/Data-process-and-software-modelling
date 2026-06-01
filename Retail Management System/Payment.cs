using System;

namespace Retail_Management_System
{
    public class Payment
    {
        public int paymentID;
        public double amount;
        public string status;

        // FR11: Validate payment details
        public bool validatePayment(string paymentDetails)
        {
            Console.WriteLine("  [PAYMENT] Validating payment details...");
            return !string.IsNullOrWhiteSpace(paymentDetails);
        }

        // FR10: Process payment - default method (no strategy)
        public bool processPayment()
        {
            Console.WriteLine($"  [PAYMENT] Processing ${amount:F2} via gateway [TLS 1.3]...");
            validatePayment("details");
            bool approved = amount <= 1000;
            status = approved ? "Success" : "Failed";
            Console.WriteLine($"  [PAYMENT] Gateway: {(approved ? "Approved" : "Declined")}. Status: {status}.");
            if (!approved)
                Console.WriteLine("  [PAYMENT] Customer notified. Retry window: 24 hours.");
            return approved;
        }

        // FR10: Process payment with Strategy pattern
        public bool processPayment(double amt, IPaymentStrategy strategy)
        {
            amount = amt;
            Console.WriteLine($"  [PAYMENT] Payment ID: {paymentID}");
            validatePayment("details");
            bool result = strategy.Pay(amount);
            status = result ? "Success" : "Failed";
            Console.WriteLine($"  [PAYMENT] Final status: {status}");
            if (!result)
                Console.WriteLine("  [PAYMENT] FR13: Customer notified of failure. Retry window: 24 hours.");
            return result;
        }
    }
}
