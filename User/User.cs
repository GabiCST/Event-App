using System.Text.RegularExpressions;

namespace Event_App
{
    public class User(string username, string email, string password, string confpassword)
    {
        private const int UserMinLength = 4;
        private const int UserMaxLength = 20;
        private const int PasswordMinLength = 6;

        private static readonly Regex UserRegex = new (@"^[a-zA-Z0-9_]+$");
        private static readonly Regex EmailRegex = new (@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string ConfirmPassword { get; set; } = confpassword;
        public string? UserIsValid()
        {
            if (string.IsNullOrWhiteSpace(Username))
                return "Username cannot be empty.";
            if (Username.Length < UserMinLength)
                return $"Username must be at least {UserMinLength} characters long.";
            if (Username.Length > UserMaxLength)
                return $"Username cannot be longer than {UserMaxLength} characters.";
            if (!UserRegex.IsMatch(Username))
                return "Username can only contain letters, numbers and underscores.";

            if (string.IsNullOrWhiteSpace(Email))
                return "Email cannot be empty.";
            if (!EmailRegex.IsMatch(Email))
                return "Email format is invalid.";

            if (string.IsNullOrWhiteSpace(Password))
                return "Password cannot be empty.";
            if (Password.Length < PasswordMinLength)
                return $"Password must be at least {PasswordMinLength} characters long.";
            if (Password != ConfirmPassword)
                return "Passwords do not match.";
            return null;
        }
    }
}
