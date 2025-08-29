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
