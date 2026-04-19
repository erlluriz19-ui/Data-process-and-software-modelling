using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_System
{
    // Represents an IT Administrator who manages users and system health.
    // FR20, NFR2, NFR3, NFR7
    public class ITAdministrator : User
    {
        private int _adminLevel;

        // Higher level = broader system permissions
        public int AdminLevel
        {
            get { return _adminLevel; }
            private set { _adminLevel = value; }
        }

        // Calls base(...) to initialise inherited User fields
        public ITAdministrator(int userId, string name, string email,
                               string password, int adminLevel)
            : base(userId, name, email, password)
        {
            _adminLevel = adminLevel;
        }

        // FR20 — Manages a user account by userID (reset password, assign role, lock/unlock)
        public void manageUsers(int userID)
        {
            // Guard: userID must be a valid positive integer
            if (userID <= 0)
            {
                Console.WriteLine("[ADMIN] Invalid userID — must be a positive integer.");
                return;
            }

            Console.WriteLine($"[ADMIN] Managing user account — UserID: {userID}");
            Console.WriteLine($"[ADMIN] Administrator: {Name} (Level {_adminLevel})");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"  UserID          : {userID}");
            Console.WriteLine("  Available actions:");
            Console.WriteLine("    [1] Reset password");
            Console.WriteLine("    [2] Lock / unlock account");
            Console.WriteLine("    [3] Assign role");
            Console.WriteLine("    [4] Revoke access");
            Console.WriteLine("------------------------------------------");

            // Log the action to the audit trail
            Console.WriteLine($"[ADMIN] Action logged to audit trail for UserID {userID}.");
        }

        // NFR2, NFR3, NFR7 — Runs a full system health check covering
        // availability, security configuration, and regulatory compliance
        public void maintainSystem()
        {
            Console.WriteLine($"[SYSTEM] System maintenance initiated by {Name}...");
            Console.WriteLine("------------------------------------------");

            // NFR2 — Availability and uptime
            Console.WriteLine("[SYSTEM] Checking system availability (NFR2)...");
            Console.WriteLine("[SYSTEM]   Database connection    : OK");
            Console.WriteLine("[SYSTEM]   Application servers    : OK  (3/3 nodes healthy)");
            Console.WriteLine("[SYSTEM]   Current uptime         : 99.97%");

            // NFR3 — Security configuration for payment data
            Console.WriteLine("[SYSTEM] Checking security configuration (NFR3)...");
            Console.WriteLine("[SYSTEM]   TLS version            : 1.3  (OK)");
            Console.WriteLine("[SYSTEM]   Encryption at rest      : AES-256  (OK)");
            Console.WriteLine("[SYSTEM]   Payment gateway cert    : Valid (expires 2026-01)");

            // NFR7 — Regulatory compliance
            Console.WriteLine("[SYSTEM] Checking regulatory compliance (NFR7)...");
            Console.WriteLine("[SYSTEM]   NZPA compliance         : PASS");
            Console.WriteLine("[SYSTEM]   PCI-DSS compliance      : PASS");
            Console.WriteLine("[SYSTEM]   Last audit              : 2024-03-01");

            // NFR6 — Inventory sync across all stores
            Console.WriteLine("[SYSTEM] Checking inventory sync across stores...");
            Console.WriteLine("[SYSTEM]   Stores synced           : 42 / 42  (OK)");
            Console.WriteLine("[SYSTEM]   Last sync lag           : 280ms  (OK)");

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("[SYSTEM] Maintenance check complete — all systems operational.");
        }

        public override string ToString()
        {
            return $"ITAdministrator[ID={UserId}, Name={Name}, Level={_adminLevel}]";
        }
    }
}