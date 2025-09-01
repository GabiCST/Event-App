using System.IO;

namespace Event_App
{
    internal class Admin
    {
        private static readonly string file = "Admins.txt";

        public static bool IsAdmin(string username)
        {
            if (!File.Exists(file)) return false;
            var admins = File.ReadAllLines(file).Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line));
            return admins.Any(admin => admin.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
