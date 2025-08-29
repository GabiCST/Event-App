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
    /// <summary>
    /// Interaction logic for Reset_Password.xaml
    /// </summary>
    public partial class Reset_Password : Window
    {
        public Reset_Password()
        {
            InitializeComponent();
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void Reset_Button(object sender, RoutedEventArgs e)
        {
             
        }
    }
}
