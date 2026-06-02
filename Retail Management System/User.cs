using System;

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
                Console.WriteLine($"  [AUTH] Login successful. Welcome, {name}!");
                return true;
            }
            Console.WriteLine("  [AUTH] Invalid Credential(s).");
            return false;
        }

        public void logout()
        {
            Console.WriteLine($"  [AUTH] Logout successful. Goodbye, {name}!");
        }
    }
}
