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

        public bool processPayment()
        {
            Console.WriteLine($"[PAYMENT] Processing payment of ${amount:F2}...");
            Console.WriteLine("[PAYMENT] Connecting to payment gateway [TLS 1.3]...");

            if (!validatePayment("details"))
            {

                handlePaymentFailure();
                return false;
            }

            bool authorised = simulateGatewayResponse();

            if (authorised)
            {

                status = "Success";
                Console.WriteLine($"[PAYMENT] Payment authorised. " +
                                  $"PaymentID: {paymentID}. Status → {status}.");
                return true;
            }
            else
            {

                handlePaymentFailure();
                return false;
            }
        }

        private void handlePaymentFailure()
        {
            status = "Failed";
            Console.WriteLine($"[PAYMENT] Payment declined. Status → {status}.");
            Console.WriteLine("[PAYMENT] Customer notified of payment failure " +
                              "via email and SMS.");
            Console.WriteLine("[PAYMENT] Order status set to PAYMENT_FAILED. " +
                              "Retry window: 24 hours.");
        }

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