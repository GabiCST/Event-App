using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
    public partial class ViewTickets : Window
    {
        public ViewTickets()
        {
            InitializeComponent(); 
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel panel = new();
            panel.Show();
            this.Close();
        }
        
    }
}
