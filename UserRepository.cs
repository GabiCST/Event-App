using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Event_App
{
    public class UserRepository
    {
        private static readonly string file = "users.txt";

        public static bool AddUser(User user)
        {
             if(FindByEmail(user.Email) != null || FindByUsername(user.Username)!= null)
                return false;
             string line = $"{user.Username}:{user.Email}:{user.Password}";
             File.AppendAllLines(file, new[] { line });
             return true;
        }
        public static User? FindByEmail(string email)
        {
            if(!File.Exists(file)) return null;
            return File.ReadAllLines(file).Select(ParseUserLine).FirstOrDefault(u => u!= null && u.Email == email);
        }
        public static User? FindByUsername(string username)
        {
            if (!File.Exists(file)) return null;
            return File.ReadAllLines(file).Select(ParseUserLine).FirstOrDefault(u => u != null && u.Username == username);
        }
        public static List<User> GetAllUsers()
        {
            if (!File.Exists(file)) return new List<User>();
            return File.ReadAllLines(file).Select(ParseUserLine).Where(u => u != null).ToList()!;
        }
        private static User? ParseUserLine(string line)
        {
            var parts = line.Split(':');
            if (parts.Length != 3) return null;
            return new User(parts[0], parts[1], parts[2], parts[2]);
        }
        public static bool ValidateCredentials(string usernameOrEmail, string password)
        {
            if (!File.Exists(file)) return false;
            return File.ReadAllLines(file).Select(ParseUserLine).Any(u => u != null && (u.Email == usernameOrEmail || u.Username == usernameOrEmail) && u.Password == password);
        }
    }
}
