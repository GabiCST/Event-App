using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {

            // Verifica existenta email ului

            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if(!IsUserValid()) return;
            if(!IsEmailValid()) return;
            if(!IsPasswordValid()) return;

            // Adauga user
            
            MainPanel mainPanel = new();
            mainPanel.Show();
            this.Close();
        }
        private bool IsUserValid()
        {
           
            if(string.IsNullOrWhiteSpace(Username.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Username.Text.Length < 4)
            {
                MessageBox.Show("Username must be at least 4 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Username.Text.Length > 20)
            {
                MessageBox.Show("Username cannot be longer than 20 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(!Regex.IsMatch(Username.Text,@"^[A-Za-z0-9_]+$"))
            {
                MessageBox.Show("Username can only contain letters, numbers and underscores.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        return true;
            // Verifica existenta
        }
        private bool IsEmailValid()
        {
            if(string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Email cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(!Regex.IsMatch(Email.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
            // Verifica existenta
        }
        private bool IsPasswordValid()
        {
            if(string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Password.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Password.Password != ConfPassword.Password)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
