using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents a Manager who generates reports and monitors performance.
    // Extends User (Inheritance) — reuses login, logout, and shared account fields.
    // Association relationship with Report (Manager generates 0..* Reports).
    // FR17, FR18, FR19
    public class Manager : User
    {
        private string _department;

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        // Calls base(...) to initialise inherited User fields
        public Manager(int userId, string name, string email,
                       string password, string department)
            : base(userId, name, email, password)
        {
            _department = department;
        }

        // FR17, FR18 — Generates a report of the specified type.
        // Report is passed in from Program.cs
        // type = "sales" (FR17) or "inventory" (FR18)
        public Report generateReport(string type, Report report)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("[REPORT] Report type cannot be empty. " +
                                  "Please specify 'sales' or 'inventory'.");
                return null;
            }

            Console.WriteLine($"[REPORT] {Name} is generating a '{type}' report " +
                              $"for department: {_department}...");

            // Delegate data retrieval and formatting to the Report class (high cohesion)
            report.generateReport(type);

            Console.WriteLine($"[REPORT] Report generated. ReportID: {report.reportID}");
            return report;
        }

        // FR17 — Views a sales report filtered by a date range
        public void viewSalesReport(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                Console.WriteLine("[REPORT] Invalid date range: end date cannot " +
                                  "be earlier than start date.");
                return;
            }

            Console.WriteLine($"[REPORT] Viewing sales report: " +
                              $"{startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
            Console.WriteLine($"[REPORT] Department: {_department}");
            Console.WriteLine("------------------------------------------");

            int totalTransactions = 38441;
            double totalRevenue = 1240000.00;
            double averageBasket = totalRevenue / totalTransactions;

            Console.WriteLine($"  Period           : {startDate:dd MMM} - {endDate:dd MMM yyyy}");
            Console.WriteLine($"  Total Revenue    : ${totalRevenue:N2}");
            Console.WriteLine($"  Transactions     : {totalTransactions:N0}");
            Console.WriteLine($"  Average Basket   : ${averageBasket:F2}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("[REPORT] Sales report displayed successfully.");
        }

        public override string ToString()
        {
            return $"Manager[ID={UserId}, Name={Name}, Department={_department}]";
        }
    }
}