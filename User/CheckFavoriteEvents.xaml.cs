using System.Windows;

namespace Event_App
{
    public partial class CheckFavoriteEvents : Window
    {
        public CheckFavoriteEvents()
        {
            InitializeComponent();
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanelWindow = new();
            mainPanelWindow.Show();
            this.Close();
        }
    }
}
