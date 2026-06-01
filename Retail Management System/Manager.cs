using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class Manager : User
    {
        public string department;

        public Manager(int userID, string name, string email, string password, string department)
            : base(userID, name, email, password)
        {
            this.department = department;
        }

        // FR17: Generate a report of a given type
        public Report generateReport(string type)
        {
            Report r = new Report
            {
                reportID = new Random().Next(1000, 9999),
                type = type
            };
            r.generateReport(type);
            return r;
        }

        // FR18: View sales report between two dates
        public void viewSalesReport(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"  [MANAGER] Sales Report: {startDate:dd-MM-yyyy} to {endDate:dd-MM-yyyy}");
            Console.WriteLine($"  [MANAGER] Department: {department}");
            Console.WriteLine("  [MANAGER] Total transactions: 142  |  Revenue: $28,430.00");
            Console.WriteLine("  [MANAGER] Top product: Kambrook Toaster (34 units)");
        }
    }
}
