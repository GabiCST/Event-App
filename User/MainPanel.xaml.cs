using System.Windows; 

namespace Event_App
{
    public partial class MainPanel : Window
    {
        public MainPanel()
        {
            InitializeComponent(); 
            if (UserSession.CurrentUser != null && UserSession.CurrentUser.Role.Equals("Admin",StringComparison.OrdinalIgnoreCase)) AdminPanelButton.Visibility = Visibility.Visible;
            else AdminPanelButton.Visibility = Visibility.Collapsed;

        }        
        private void Favorite_Button(object sender, RoutedEventArgs e)
        {
                ViewFavoriteTickets favoriteTicketsWindow = new();
                favoriteTicketsWindow.Show();
                this.Close();
        }
        private void Purchased_Button(object sender, RoutedEventArgs e)
        {
            ViewPurchasedTickets purchasedTicketsWindow = new();
            purchasedTicketsWindow.Show();
            this.Close();
        }

        private void Events_Button(object sender, RoutedEventArgs e)
        {
             ViewTickets viewTicketsWindow = new();
             viewTicketsWindow.Show();
             this.Close();
        }
        private void LogOut_Button(object sender, RoutedEventArgs e)
        {
            UserSession.clearUser();
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void AdminPanel_Button(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentUser == null || !UserSession.CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Access denied. Admin privileges required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AdminWindow admin = new();
            admin.Show();
            this.Close();
        }
    }
}
