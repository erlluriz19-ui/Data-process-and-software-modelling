using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class Manager : User
    {
        public string department;

        public Report generateReport(string type)
        {
            return new Report { type = type };
        }
        public void viewSalesReport (DateTime startDate, DateTime endDate)
        {

        }
    }
}
