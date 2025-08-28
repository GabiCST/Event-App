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
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Forgot_Password(object sender, RoutedEventArgs e)
        {

        }
        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();
        }
    }
}
