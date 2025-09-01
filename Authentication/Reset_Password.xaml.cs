using System.Windows;

namespace Event_App
{
    public partial class Reset_Password : Window
    {
        private readonly User _user;

        private const int PasswordMinLength = 6;
        public Reset_Password(User user)
        {
            _user = user;
            InitializeComponent();
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Account_Verification account_Verification = new();
            account_Verification.Show();
            this.Close();
        }
        private void Reset_Button(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Password.Password) || string.IsNullOrWhiteSpace(ConfPassword.Password))
            {
                MessageBox.Show("Please provide a new password and confirm it", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Password.Password.Length < PasswordMinLength)
            {
                MessageBox.Show($"Password must be at least {PasswordMinLength} characters long.");
                return;
            }
            if (Password.Password != ConfPassword.Password)
            {
                MessageBox.Show("Passwords are not the same.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (UserRepository.PasswordChange(_user, Password.Password))
            {
                MessageBox.Show("Password was updated succesfully");
                Login login = new();
                login.Show();
                this.Close();
            }
            else MessageBox.Show("An unexpected error occured while changing your password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
