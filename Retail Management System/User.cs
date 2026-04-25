using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    public class User
    {
        public int userID;
        public string name;
        public string email;
        public string password;

        public User(int userID, string name, string email, string password)
        {
            this.userID = userID;
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public bool login(string email, string password)
        {
            if (this.email == email && this.password == password)
            {
                Console.WriteLine($"[Authentication]: Login successful. Welcome, {name}!");
                return true;
            }
            else
            {
                Console.WriteLine("[Authentication]: Login failed. Invalid email or password.");
                return false;
            }
        }
        public void logout()
        {
            Console.WriteLine($"[Authentication]: Logout successful. Goodbye, {name}!");
        }
    }
}
