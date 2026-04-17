using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class User
    {
        public int userId;
        public string name;
        public string email;
        public string password;

        public bool login(string email, string password)
        {
            return this.email == email && this.password == password;
        }

        public void logout()
        {

        }
    }
}
