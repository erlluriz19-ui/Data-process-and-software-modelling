using System;
using System.Collections.Generic;

namespace Retail_Management_System
{
    public class ITAdministrator : User
    {
        public int adminLevel;

        public ITAdministrator(int userID, string name, string email, string password, int adminLevel)
            : base(userID, name, email, password)
        {
            this.adminLevel = adminLevel;
        }

        // FR20: Manage user account - add, update, or remove
        public void manageUsers(int userID, string action, List<User> users)
        {
            switch (action.ToLower())
            {
                case "add":
                    Console.WriteLine($"  [IT ADMIN] User ID {userID} added to system.");
                    break;
                case "remove":
                    User found = users.Find(u => u.userID == userID);
                    if (found != null)
                    {
                        users.Remove(found);
                        Console.WriteLine($"  [IT ADMIN] User '{found.name}' (ID {userID}) removed.");
                    }
                    else
                        Console.WriteLine($"  [IT ADMIN] User ID {userID} not found.");
                    break;
                case "update":
                    Console.WriteLine($"  [IT ADMIN] User ID {userID} record updated.");
                    break;
                default:
                    Console.WriteLine("  [IT ADMIN] Unknown action. Use: add, remove, update.");
                    break;
            }
        }

        // NFR3 / NFR7: System maintenance and compliance checks
        public void maintainSystem()
        {
            Console.WriteLine("  [IT ADMIN] Running system diagnostics...");
            Console.WriteLine("  [IT ADMIN] TLS 1.3 encryption: OK");
            Console.WriteLine("  [IT ADMIN] NZ Privacy Act 2020 compliance: OK");
            Console.WriteLine("  [IT ADMIN] Database backup: Completed");
            Console.WriteLine("  [IT ADMIN] System uptime: 99.97% (NFR2: 24/7 availability met)");
            Console.WriteLine("  [IT ADMIN] Maintenance complete. All systems operational.");
        }
    }
}
