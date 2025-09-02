using System.Windows; 

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
                
        }
        private void Purchased_Tickets_Button(object sender, RoutedEventArgs e)
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
