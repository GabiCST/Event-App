using System.Windows;

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

            if (!UserRepository.ValidateCredentialsPassword(Username.Text, Email.Text))
            {
                MessageBox.Show("Invalid username or email.", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User user = new(0,Username.Text, Email.Text, string.Empty, string.Empty, string.Empty);

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
