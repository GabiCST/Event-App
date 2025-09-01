using System.Windows;

namespace Event_App
{
    public partial class Events : Window
    {
        public Events()
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
