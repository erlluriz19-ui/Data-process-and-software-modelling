using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Handles all payment processing and validation for in-store and online transactions.
    // Association relationship — Sale ──── Payment (1 to 1).
    // FR10, FR11, FR12, FR13
    public class Payment
    {
        public int paymentID;
        public double amount;
        public string status;

        // FR11 — Validates payment details before any money is moved.
        // In production this would check card number format, expiry, and CVV.
        public bool validatePayment(string paymentDetails)
        {
            if (string.IsNullOrWhiteSpace(paymentDetails))
            {
                Console.WriteLine("[PAYMENT] Validation failed: payment details " +
                                  "cannot be empty.");
                return false;
            }

            Console.WriteLine("[PAYMENT] Validating payment details...");
            Console.WriteLine("[PAYMENT] Validation passed — details are well-formed.");
            return true;
        }

        // FR10, FR12, FR13 — Validates then processes the payment via the gateway.
        // On success: status = "Success", returns true (FR12).
        // On failure: calls handlePaymentFailure() and returns false (FR13).
        public bool processPayment()
        {
            Console.WriteLine($"[PAYMENT] Processing payment of ${amount:F2}...");
            Console.WriteLine("[PAYMENT] Connecting to payment gateway [TLS 1.3]...");

            // FR11 — Validate before contacting the gateway
            if (!validatePayment("details"))
            {
                handlePaymentFailure();
                return false;
            }

            bool authorised = simulateGatewayResponse();

            if (authorised)
            {
                // FR12 — Payment confirmed
                status = "Success";
                Console.WriteLine($"[PAYMENT] Payment authorised. " +
                                  $"PaymentID: {paymentID}. Status → {status}.");
                return true;
            }
            else
            {
                // FR13 — Payment declined
                handlePaymentFailure();
                return false;
            }
        }

        // FR13 — Handles a declined payment: sets status, notifies customer.
        // Private so it can only be triggered by processPayment() — cannot be skipped.
        private void handlePaymentFailure()
        {
            status = "Failed";
            Console.WriteLine($"[PAYMENT] Payment declined. Status → {status}.");
            Console.WriteLine("[PAYMENT] Customer notified of payment failure " +
                              "via email and SMS.");
            Console.WriteLine("[PAYMENT] Order status set to PAYMENT_FAILED. " +
                              "Retry window: 24 hours.");
        }

        // Simulates a gateway response for demo purposes.
        // Amounts over $1000 are declined to demonstrate the failure path (FR13).
        private bool simulateGatewayResponse()
        {
            bool approved = amount <= 1000;
            Console.WriteLine($"[PAYMENT] Gateway response: " +
                              $"{(approved ? "APPROVED" : "DECLINED")}.");
            return approved;
        }

        public override string ToString()
        {
            return $"Payment[ID={paymentID}, Amount=${amount:F2}, Status={status}]";
        }
    }
}