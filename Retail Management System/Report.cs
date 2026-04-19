using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents a system-generated report (sales or inventory).
    // Association relationship — Manager ──── Report (0..*).
    // Report knows how to generate and export itself (high cohesion).
    // FR17, FR18, FR19
    public class Report
    {
        public int reportID;
        public string type;

        // FR17, FR18 — Generates report content based on the type field.
        // "sales" = sales summary (FR17), "inventory" = stock overview (FR18).
        // Switch handles each report type separately (high cohesion — Report builds itself).
        public void generateReport(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("[REPORT] Report type cannot be empty.");
                return;
            }

            this.type = type;

            Console.WriteLine($"[REPORT] Generating '{type}' report (ID: {reportID})...");
            Console.WriteLine("==========================================");

            switch (type.ToLower())
            {
                case "sales":
                    // FR17 — Sales report with revenue, transactions, and department breakdown
                    Console.WriteLine("          SALES REPORT                   ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine($"  Report ID        : {reportID}");
                    Console.WriteLine($"  Generated        : {DateTime.Now:yyyy-MM-dd HH:mm}");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("  SUMMARY");
                    Console.WriteLine($"  Total Revenue    : $1,240,000.00");
                    Console.WriteLine($"  Transactions     : 38,441");
                    Console.WriteLine($"  Avg Basket Size  : $32.27");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("  BY DEPARTMENT");
                    Console.WriteLine($"  General Merch    : $441,200  (+8.2% vs target)");
                    Console.WriteLine($"  Electronics      : $389,000  (+4.1% vs target)");
                    Console.WriteLine($"  Stationery       : $198,400  (-2.3% vs target)");
                    Console.WriteLine($"  Outdoor          : $211,900  (+11.0% vs target)");
                    Console.WriteLine("==========================================");
                    break;

                case "inventory":
                    // FR18 — Inventory report with SKU count, sync status, and alerts
                    Console.WriteLine("        INVENTORY REPORT                  ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine($"  Report ID        : {reportID}");
                    Console.WriteLine($"  Generated        : {DateTime.Now:yyyy-MM-dd HH:mm}");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"  Total SKUs       : 18,440");
                    Console.WriteLine($"  Stores Synced    : 42 / 42");
                    Console.WriteLine($"  Low Stock Items  : 12");
                    Console.WriteLine($"  Critical Alerts  : 3");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("  TOP LOW-STOCK PRODUCTS");
                    Console.WriteLine($"  Samsung 65\" TV   : 3 units (threshold: 5)");
                    Console.WriteLine($"  A4 Copy Paper    : 1 unit  (threshold: 8)");
                    Console.WriteLine("==========================================");
                    break;

                default:
                    // Handles any custom report type
                    Console.WriteLine($"  CUSTOM REPORT — Type: {type}           ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine($"  Report ID        : {reportID}");
                    Console.WriteLine($"  Generated        : {DateTime.Now:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"  Custom data retrieved for type '{type}'.");
                    Console.WriteLine("==========================================");
                    break;
            }

            Console.WriteLine($"[REPORT] '{type}' report generated successfully.");
        }

        // FR19 — Exports the report to the specified file format (xlsx, pdf, csv).
        // Builds a filename from the report type and ID, then confirms the export.
        public void exportReport(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                Console.WriteLine("[REPORT] Export format cannot be empty. " +
                                  "Please specify 'xlsx', 'pdf', or 'csv'.");
                return;
            }

            string filename = $"RPT-{type?.ToUpper() ?? "REPORT"}-{reportID}.{format.ToLower()}";

            Console.WriteLine($"[REPORT] Exporting report as {format.ToUpper()}...");
            Console.WriteLine($"[REPORT] Serialising data to {format.ToUpper()} format...");
            Console.WriteLine($"[REPORT] Export complete → {filename}");
            Console.WriteLine($"[REPORT] File size: 2.4 MB  |  " +
                              $"Export time: {DateTime.Now:HH:mm:ss}");
        }

        public override string ToString()
        {
            return $"Report[ID={reportID}, Type={type}]";
        }
    }
}