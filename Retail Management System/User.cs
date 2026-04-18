using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{

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

        protected string Password
        {
            set { _password = value; }
        }

        public User(int userId, string name, string email, string password)
        {
            _userId = userId;
            _name = name;
            _email = email;
            _password = password;
        }

        public bool login(string email, string password)
        {

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("[AUTH] Login failed: email or password cannot be empty.");
                return false;
            }

            if (_email == email && _password == password)
            {
                Console.WriteLine($"[AUTH] Login successful. Welcome, {_name}!");
                return true;
            }

            Console.WriteLine("[AUTH] Login failed: invalid email or password.");
            return false;
        }

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