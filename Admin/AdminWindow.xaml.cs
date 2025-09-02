using System.Windows;

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
        private void EventDeletion_Button(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("User information is missing. Cannot delete events.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                EventDeletion eventDeletion = new(_user);
                eventDeletion.Show();
                this.Close();
            }
        }
        private void ViewCreatedAccounts_Button(object sender, RoutedEventArgs e)
        { 
        }
    }
}
