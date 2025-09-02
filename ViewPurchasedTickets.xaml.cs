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
using System.IO;

namespace Event_App
{
    /// <summary>
    /// Interaction logic for ViewPurchasedTickets.xaml
    /// </summary>
    public partial class ViewPurchasedTickets : Window
    {
        public ViewPurchasedTickets()
        {
            InitializeComponent();
            LoadTickets("bought_tickets.txt");
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel panel = new();
            panel.Show();
            this.Close();
        }
        private void LoadTickets(string file)
        {
            if (!File.Exists(file))
            {
                EventsPanel.Children.Add(new TextBlock { Text = "No events avaliable" });
                return;
            }
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 5) AddEvent(parts[0], parts[1], parts[2], parts[3], parts[4]);

            }
        }
        private void AddEvent(string Type, string title, string date, string time, string nrTickets)
        {
            Border card = new()
            {
                BorderBrush = System.Windows.Media.Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(5),
                Padding = new Thickness(10)
            };
            StackPanel panel = new();
            panel.Children.Add(new TextBlock { Text = $"Event Type:{Type}" });
            panel.Children.Add(new TextBlock { Text = $"Event: {title}", FontWeight = FontWeights.Bold });
            panel.Children.Add(new TextBlock { Text = $"Date: {date}" });
            panel.Children.Add(new TextBlock { Text = $"Time: {time}" });
            panel.Children.Add(new TextBlock { Text = $"Available Tickets: {nrTickets}" });
            card.Child = panel;
            EventsPanel.Children.Add(card);
        }
    }
}
