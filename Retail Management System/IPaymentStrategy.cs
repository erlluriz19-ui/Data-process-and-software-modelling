using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public interface IPaymentStrategy
    {
        bool Pay(double amount);
    }
}
