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
            var user = new User(Username.Text, Email.Text, Password.Password, ConfPassword.Password);

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

            MainPanel mainPanel = new(Username.Text);
            mainPanel.Show();
            this.Close();
        }
    }
}
