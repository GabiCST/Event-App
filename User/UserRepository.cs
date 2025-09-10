using System.Data.OleDb;

namespace Event_App
{
    public class UserRepository
    {

        private static readonly string connectionString = App.Configuration["ConnectionStrings:DefaultConnection"];
        public static bool AddUser(User user)
        {
            if (FindUser(user.Username,user.Email) != null)
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
            var user = FindUser(usernameOrEmail, usernameOrEmail);
            if(user != null && user.Password == password) return user;
            return null;
        }

        public static bool PasswordChange(User user, string password)
        {
            var existinguser = FindUser(user.Username, user.Email);
            if (existinguser == null) return false;
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "UPDATE users " +
                "SET [password]=? " +
                "WHERE [user_id]=?", conn);
            cmd.Parameters.AddWithValue("?", password); 
            cmd.Parameters.AddWithValue("?", existinguser.Id);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        } 
        private static User? FindUser(string? username = null, string? email = null)
        {
            if(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
                throw new ArgumentException("At least one parameter must be provided.");
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            string query = 
                "SELECT [user_id],[username], [email], [password], [role],[date_added],[money] " +
                "FROM users WHERE ";
            var conditions = new List<string>();
            if (!string.IsNullOrEmpty(username))  conditions.Add("[username]=?");
            if (!string.IsNullOrEmpty(email))     conditions.Add("[email]=?");
            
            query += string.Join(" OR ", conditions);
            
            using var cmd = new OleDbCommand(query, conn);
            if (!string.IsNullOrEmpty(username))  cmd.Parameters.AddWithValue("?", username);
            if (!string.IsNullOrEmpty(email)) cmd.Parameters.AddWithValue("?", email);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(Convert.ToInt32(reader["user_id"]),
                                  reader["username"].ToString(),
                                  reader["email"].ToString(),
                                  reader["password"].ToString(),
                                  reader["password"].ToString(),
                                  reader["role"].ToString(),
                                  Convert.ToDateTime(reader["date_added"]),
                                  Convert.ToInt32(reader["money"]));
            }
            return null;
        }
        

        public static List<User> GetAllUsers()
        {
            List<User> users = new();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [user_id],[username], [email], [password], [role],[date_added],[money] " +
                "FROM users", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User(Convert.ToInt32(reader["user_id"]),
                                  reader["username"].ToString(),
                                  reader["email"].ToString(),
                                  reader["password"].ToString(),
                                  reader["password"].ToString(),
                                  reader["role"].ToString(),
                                  Convert.ToDateTime(reader["date_added"]),
                                  Convert.ToInt32(reader["money"])));
            }
            return users;
        }
        
    }
}
