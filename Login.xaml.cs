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
            if(string.IsNullOrWhiteSpace(UserOrEmail.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Please enter both email and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!UserRepository.ValidateCredentials(UserOrEmail.Text, Password.Password))
            {
                MessageBox.Show("Invalid email or password.", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainPanel mainPanelWindow = new();
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
