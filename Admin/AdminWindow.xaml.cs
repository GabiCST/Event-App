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

    public partial class AdminWindow : Window
    {
        private readonly User? _user;
        public AdminWindow(User user)
        {
            _user = user;
            InitializeComponent();
        }
        public void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanel = new(_user?.Username ?? string.Empty);
            mainPanel.Show();
            this.Close();
        }
        public void LogOut_Button(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void AddTickets_Button(object sender, RoutedEventArgs e)
        {
            if (_user == null)
    {
        MessageBox.Show("User information is missing. Cannot add tickets.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }
    AddTickets addticket = new(_user);
    addticket.Show();
    this.Close();
        }
        private void AvailableTickets_Button(object sender, RoutedEventArgs e)
        { 
        }
        private void ViewCreatedAccounts_Button(object sender, RoutedEventArgs e)
        { 
        }
    }
}
