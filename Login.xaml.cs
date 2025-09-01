using Microsoft.Win32;
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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

        }
        private void Login_Button(object sender, RoutedEventArgs e)
        {
            var input = UserOrEmail.Text?.Trim();
            var pwd = Password.Password;

            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("Please enter both Username or email and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = UserRepository.Authenticate(input, pwd);
            if (user == null)
            {
                MessageBox.Show("Invalid user/email or password.", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 
            MainPanel mainPanelWindow = new(user.Username);
            mainPanelWindow.Show();
            this.Close();
        }
        private void Forgot_Password(object sender, RoutedEventArgs e)
        {
            Account_Verification accountVerificationWindow = new();
            accountVerificationWindow.Show();
            this.Close();
        }
        private void Register_Button(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new();
            registerWindow.Show();
            this.Close();
        }
    }
}
