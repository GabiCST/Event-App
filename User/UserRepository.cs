using Microsoft.Extensions.Configuration;
using System.Data.OleDb;

namespace Event_App
{
    public class UserRepository
    {

        private static readonly string connectionString = App.Configuration["ConnectionStrings:DefaultConnection"];
        public static bool AddUser(User user)
        {
            if (FindByUsername(user.Username) != null || FindByEmail(user.Email) != null)
                return false;

            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
            "INSERT INTO users ([username], [email], [password], [role], [date_added]) " +
            "VALUES (?,?,?,?,?)", conn);
            cmd.Parameters.AddWithValue("?", user.Username);
            cmd.Parameters.AddWithValue("?", user.Email);
            cmd.Parameters.AddWithValue("?", user.Password);
            cmd.Parameters.AddWithValue("?", user.Role);
            cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public static User? Authenticate(string usernameOrEmail, string password)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [user_id],[username], [email], [password],[role],[date_added] " +
                "FROM users " +
                "WHERE ([username]=? or [email]=?) AND [password]=?", conn);
            cmd.Parameters.AddWithValue("?", usernameOrEmail);
            cmd.Parameters.AddWithValue("?", usernameOrEmail);
            cmd.Parameters.AddWithValue("?", password);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(Convert.ToInt32(reader["user_id"]),
                                  reader["username"].ToString(),
                                  reader["email"].ToString(),
                                  reader["password"].ToString(),
                                  reader["password"].ToString(),
                                  reader["role"].ToString(),
                                  Convert.ToDateTime(reader["date_added"]));
            }
                return null;
        }

        public static bool PasswordChange(User user, string password)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "UPDATE users " +
                "SET [password]=? " +
                "WHERE [username]=? AND [email]=?", conn);
            cmd.Parameters.AddWithValue("?", password); 
            cmd.Parameters.AddWithValue("?", user.Username);
            cmd.Parameters.AddWithValue("?", user.Email);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }


        private static User? FindByEmail(string email)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [username], [email], [password], [role],[date_added]  " +
                "FROM users " +
                "WHERE [email]=?", conn);
            cmd.Parameters.AddWithValue("?", email);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(Convert.ToInt32(reader["user_id"]),
                                reader["username"].ToString(),
                                reader["email"].ToString(),
                                reader["password"].ToString(),
                                reader["password"].ToString(),
                                reader["role"].ToString(),
                                Convert.ToDateTime(reader["date_added"]));
            }
            return null;
        }
        private static User? FindByUsername(string username)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [username], [email], [password], [role] ,[date_added] " +
                "FROM users " +
                "WHERE [username]=?", conn);
            cmd.Parameters.AddWithValue("?", username);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(Convert.ToInt32(reader["user_id"]),
                                  reader["username"].ToString(),
                                  reader["email"].ToString(),
                                  reader["password"].ToString(),
                                  reader["password"].ToString(),
                                  reader["role"].ToString(),
                                  Convert.ToDateTime(reader["date_added"]));
            }
            return null;
        }
    }
}
