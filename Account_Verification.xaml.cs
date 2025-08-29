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
            Reset_Password resetPasswordWindow = new();
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
