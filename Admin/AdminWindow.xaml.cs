using System.Windows;

namespace Event_App
{

    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }
        public void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanel = new();
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
            if (UserSession.CurrentUser == null || !UserSession.CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Access denied. Admin privileges required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddTickets addticket = new();
            addticket.Show();
            this.Close();
        }
        private void AvailableTickets_Button(object sender, RoutedEventArgs e)
        { 
        }
        private void EventDeletion_Button(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentUser == null || !UserSession.CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Access denied. Admin privileges required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            EventDeletion eventDeletion = new();
            eventDeletion.Show();
            this.Close();
        }
        private void ViewCreatedAccounts_Button(object sender, RoutedEventArgs e)
        { 
        }
    }
}
