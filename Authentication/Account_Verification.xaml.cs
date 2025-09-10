using System.Windows;
using System.Data.OleDb;
namespace Event_App
{
    public partial class Account_Verification : Window
    {
        public Account_Verification()
        {
            InitializeComponent();
        }

        private void Verify_Button(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Please enter both username and email.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? user;
            using var conn = new OleDbConnection(App.Configuration["ConnectionStrings:DefaultConnection"]);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [user_id],[username], [email], [password], [role],[date_added],[money]  " +
                "FROM users " +
                "WHERE username=? AND email=?", conn);
            cmd.Parameters.AddWithValue("?", Username.Text);
            cmd.Parameters.AddWithValue("?", Email.Text);

            using var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                user = new User(Convert.ToInt32(reader["user_id"]),
                           reader["username"].ToString(),
                           reader["email"].ToString(),
                           reader["password"].ToString(),
                           reader["password"].ToString(),
                           reader["role"].ToString(),
                           Convert.ToDateTime(reader["date_added"]),
                           Convert.ToInt32(reader["money"]));
                          
            }

            else
            {
                MessageBox.Show("Invalid username or email.", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Reset_Password resetPasswordWindow = new(user);
            resetPasswordWindow.Show();
            this.Close();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
    }
}
