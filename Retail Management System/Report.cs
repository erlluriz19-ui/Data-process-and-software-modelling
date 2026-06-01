using System;

namespace Retail_Management_System
{
    public class Report
    {
        public int reportID;
        public string type;

        // FR17/FR18/FR19: Generate report by type
        public void generateReport(string type)
        {
            Console.WriteLine($"  [REPORT] Generating {type} report (ID: {reportID})...");
            switch (type.ToLower())
            {
                case "sales":
                    Console.WriteLine("  [REPORT] Period: 01-05-2026 to 31-05-2026");
                    Console.WriteLine("  [REPORT] Total transactions : 142");
                    Console.WriteLine("  [REPORT] Total revenue      : $28,430.00");
                    Console.WriteLine("  [REPORT] Top product        : Kambrook 2-Slice Toaster (34 units)");
                    break;
                case "inventory":
                    Console.WriteLine("  [REPORT] Total products     : 48");
                    Console.WriteLine("  [REPORT] Low stock items    : 3");
                    Console.WriteLine("  [REPORT] Out of stock items : 1");
                    break;
                default:
                    Console.WriteLine("  [REPORT] General report generated.");
                    break;
            }
        }

        // FR19: Export report to a format
        public void exportReport(string format)
        {
            Console.WriteLine($"  [REPORT] Exporting report {reportID} as {format.ToUpper()}...");
            Console.WriteLine($"  [REPORT] Export complete: report_{reportID}.{format.ToLower()}");
        }
    }
}
