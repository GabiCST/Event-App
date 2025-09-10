using System.Windows;

namespace Event_App
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new User(0,Username.Text, Email.Text, Password.Password, ConfPassword.Password, "User", DateTime.Now,0);

            string? validationError = user.UserIsValid();
            if (validationError != null)
            {
                MessageBox.Show(validationError, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!UserRepository.AddUser(user))
            {
                MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            UserSession.setUser(user);
            MainPanel mainPanel = new();
            mainPanel.Show();
            this.Close();
        }
    }
}
