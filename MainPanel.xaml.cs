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
    public partial class MainPanel : Window
    {
        private readonly User? _user; 
        public MainPanel()
        {
            InitializeComponent(); 
            AdminPanelButton.Visibility = Visibility.Collapsed;
        } 
        public MainPanel(string username)
        { 
            _user = UserRepository.FindByUsername(username) ?? UserRepository.FindByEmail(username); 
            InitializeComponent(); 
            if (_user != null && Admin.IsAdmin(_user.Username))
            {
                AdminPanelButton.Visibility = Visibility.Visible;
            }
            else
            {
                AdminPanelButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Available_Tickets_Button(object sender, RoutedEventArgs e)
        {
            AvailableTickets availableTicketsWindow = new();
            availableTicketsWindow.Show();
            this.Close();
        }
        private void Favorite_Button(object sender, RoutedEventArgs e)
        {
            CheckFavoriteEvents checkFavoriteEvents = new();
            checkFavoriteEvents.Show();
            this.Close();
        }
        private void AvailableTicketAll_Button(object sender, RoutedEventArgs e)
        {
            AvailableTickets availableTicketsWindow = new();
            availableTicketsWindow.Show();
            this.Close();
        }
        private void AvailableTicketFavorite_Button(object sender, RoutedEventArgs e)
        {
            CheckFavoriteEvents checkFavoriteEvents = new();
            checkFavoriteEvents.Show();
            this.Close();
        }
        private void LogOut_Button(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void AdminPanel_Button(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new(_user);
            admin.Show();
            this.Close();
        }
    }
}
