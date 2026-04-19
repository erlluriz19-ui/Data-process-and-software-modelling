using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Base class for all users in the RMS.
    // All specialised roles inherit from this class (Inheritance).
    public class User
    {
       
        private int _userId;
        private string _name;
        private string _email;
        private string _password;

        public int UserId
        {
            get { return _userId; }
            protected set { _userId = value; }
        }

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            protected set { _email = value; }
        }

        // Password is write-only — never exposed publicly for security
        protected string Password
        {
            set { _password = value; }
        }

        // Constructor — called by all subclasses via base(...) to initialise shared fields
        public User(int userId, string name, string email, string password)
        {
            _userId = userId;
            _name = name;
            _email = email;
            _password = password;
        }

        // FR20 — Authenticates the user by comparing supplied credentials to stored values
        public bool login(string email, string password)
        {
            // Guard: reject empty credentials immediately
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("[AUTH] Login failed: email or password cannot be empty.");
                return false;
            }

            // Check credentials match
            if (_email == email && _password == password)
            {
                Console.WriteLine($"[AUTH] Login successful. Welcome, {_name}!");
                return true;
            }

            // Do not reveal which field was wrong — security best practice
            Console.WriteLine("[AUTH] Login failed: invalid email or password.");
            return false;
        }

        // Ends the user's active session
        public void logout()
        {
            Console.WriteLine($"[AUTH] {_name} has been logged out successfully.");
        }

        public override string ToString()
        {
            return $"User[ID={_userId}, Name={_name}, Email={_email}]";
        }
    }
}