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
        public MainPanel()
        {
            InitializeComponent();
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
    }
}
