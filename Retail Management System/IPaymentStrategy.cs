using System;

namespace Retail_Management_System
{
    // Strategy Pattern - Interface (FR10-FR13)
    public interface IPaymentStrategy
    {
        bool Pay(double amount);
    }

    // Concrete Strategy 1: Card payment with limit
    public class CardPayment : IPaymentStrategy
    {
        private const double CardLimit = 1000.00;

        public bool Pay(double amount)
        {
            Console.WriteLine($"  [CARD] Processing ${amount:F2} via card gateway [TLS 1.3]...");
            if (amount <= CardLimit)
            {
                Console.WriteLine("  [CARD] APPROVED");
                return true;
            }
            Console.WriteLine($"  [CARD] DECLINED - amount exceeds card limit (${CardLimit:F2}).");
            return false;
        }
    }

    // Concrete Strategy 2: Cash always succeeds
    public class CashPayment : IPaymentStrategy
    {
        public bool Pay(double amount)
        {
            Console.WriteLine($"  [CASH] Received ${amount:F2} in cash.");
            Console.WriteLine("  [CASH] Payment accepted.");
            return true;
        }
    }

    // Concrete Strategy 3: Online with higher limit
    public class OnlinePayment : IPaymentStrategy
    {
        private const double OnlineLimit = 2000.00;

        public bool Pay(double amount)
        {
            Console.WriteLine($"  [ONLINE] Processing ${amount:F2} via online gateway [TLS 1.3]...");
            if (amount <= OnlineLimit)
            {
                Console.WriteLine("  [ONLINE] APPROVED");
                return true;
            }
            Console.WriteLine($"  [ONLINE] DECLINED - amount exceeds online limit (${OnlineLimit:F2}).");
            return false;
        }
    }
}
