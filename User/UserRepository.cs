using System.IO;

namespace Event_App
{
    public class UserRepository
    {
        private static readonly string file = "users.txt";

        public static bool AddUser(User user)
        {
            if(FindByEmail(user.Email) != null || FindByUsername(user.Username)!= null) return false;
            string line = $"{user.Username}:{user.Email}:{user.Password}";
            File.AppendAllLines(file, new List<string> { line });
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
        public static bool ValidateCredentialsPassword(string username, string email)
        {
            if (!File.Exists(file)) return false;
            return File.ReadAllLines(file).Select(ParseUserLine).Any(u => u != null && u.Username == username && u.Email == email);
        }
        public static bool PasswordChange(User user, string password)
        {
            var users = GetAllUsers();
            var userToUpdate = users.FirstOrDefault(u => u != null && u.Email == user.Email);
            if (userToUpdate == null) return false;
            userToUpdate.Password = password;
            var lines = users.Select(u => $"{u.Username}:{u.Email}:{u.Password}");
            File.WriteAllLines(file,lines);
            return true;
        }

        public static User? Authenticate(string usernameOrEmail, string password)
        {
            if (!File.Exists(file)) return null;
            var input = usernameOrEmail.Trim();
            return File.ReadAllLines(file)
                .Select(ParseUserLine)
                .FirstOrDefault(u =>
                    u != null &&
                    (u.Email.Equals(input, StringComparison.OrdinalIgnoreCase) ||
                     u.Username.Equals(input, StringComparison.OrdinalIgnoreCase)) &&
                    u.Password == password);
        }

        public static string? ResolveUsername(string usernameOrEmail)
        {
            var input = usernameOrEmail.Trim();
            var user = FindByUsername(input) ?? FindByEmail(input);
            return user?.Username;
        }
    }
}
