using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            User user = new(Username.Text, Email.Text, string.Empty, string.Empty);

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
